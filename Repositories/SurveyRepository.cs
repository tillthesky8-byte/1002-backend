using _1002_backend.Models;
using _1002_backend.Infrastructure.Data;
using _1002_backend.Repositories.Interfaces;
using Dapper;

namespace _1002_backend.Repositories;

public class SurveyRepository : ISurveyRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<SurveyRepository> _logger;

    public SurveyRepository(IDbConnectionFactory dbConnectionFactory, ILogger<SurveyRepository> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }

    public async Task<bool> SurveyExistsToday()
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"SELECT COUNT(1) FROM SurveySessions WHERE Date = @Today";
            var count = await connection.ExecuteScalarAsync<int>(query, new { Today = DateOnly.FromDateTime(DateTime.UtcNow) });
            _logger.LogInformation("Checked for existing survey session today: {Exists}", count > 0);
            return count > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking for existing survey session today");
            throw;
        }
    }

    public async Task<bool> SurveySubmit(SurveySession session, List<Answer> answers)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            // Insert SurveySession
            const string insertSessionQuery = @"INSERT INTO SurveySessions (Date) VALUES (@Date); SELECT last_insert_rowid();";
            var sessionId = await connection.ExecuteScalarAsync<int>(insertSessionQuery, new { Date = session.Date }, transaction);

            // Insert Answers
            const string insertAnswerQuery = @"INSERT INTO Answers (SurveySessionId, QuestionId, Response) VALUES (@SurveySessionId, @QuestionId, @Response)";
            foreach (var answer in answers)
            {
                await connection.ExecuteAsync(insertAnswerQuery, new { SurveySessionId = sessionId, QuestionId = answer.QuestionId, Response = answer.Response }, transaction);
            }

            transaction.Commit();
            _logger.LogInformation("Submitted survey session with ID {SessionId} and {AnswerCount} answers", sessionId, answers.Count);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting survey session");
            throw;
        }
    }
}
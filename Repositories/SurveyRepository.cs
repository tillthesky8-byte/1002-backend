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
            _logger.LogInformation("\n ---START--- \n \n Connection object created for checking existing survey session today \n \n ---END--- \n");

            const string query = @"SELECT COUNT(1) FROM SurveySessions WHERE Date = @Today";
            var count = await connection.ExecuteScalarAsync<int>(query, new { Today = DateOnly.FromDateTime(DateTime.UtcNow) });
            _logger.LogInformation("\n ---START--- \n \n Checked for existing survey session today: {Exists} \n \n ---END--- \n", count > 0);
            return count > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error checking for existing survey session today \n \n ---END--- \n");
            throw;
        }
    }

    public async Task<bool> SurveySubmit(SurveySession session, List<Answer> answers)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            _logger.LogInformation("\n ---START--- \n \n Connection object created for submitting survey session \n \n ---END--- \n");

            connection.Open(); // note: upgrage to async version in the future
            _logger.LogInformation("\n ---START--- \n \n Database connection opened for submitting survey session \n \n ---END--- \n");

            using var transaction = connection.BeginTransaction();

            // Insert SurveySession
            const string insertSessionQuery = @"INSERT INTO SurveySessions (Date) VALUES (@Date); SELECT last_insert_rowid();";
            var sessionId = await connection.ExecuteScalarAsync<int>(insertSessionQuery, new { Date = session.Date }, transaction);
            _logger.LogInformation("\n ---START--- \n \n Created survey session with ID {SessionId} for date {Date} \n \n ---END--- \n", sessionId, session.Date);

            // Insert Answers
            const string insertAnswerQuery = @"INSERT INTO Answers (SurveySessionId, QuestionId, Response) VALUES (@SurveySessionId, @QuestionId, @Response)";
            foreach (var answer in answers)
            {
                await connection.ExecuteAsync(insertAnswerQuery, new { SurveySessionId = sessionId, QuestionId = answer.QuestionId, Response = answer.Response }, transaction);
            }
            _logger.LogInformation("\n ---START--- \n \n Inserted answers for survey session with ID {SessionId} \n \n ---END--- \n", sessionId);

            transaction.Commit();
            _logger.LogInformation("\n ---START--- \n \n Submitted survey session with ID {SessionId} and {AnswerCount} answers \n \n ---END--- \n", sessionId, answers.Count);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error submitting survey session \n \n ---END--- \n");
            throw;
        }
    }
}
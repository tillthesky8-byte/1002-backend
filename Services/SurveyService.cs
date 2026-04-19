using _1002_backend.Models;
using _1002_backend.Repositories.Interfaces;
using _1002_backend.Services.Interfaces;
namespace _1002_backend.Services;

public class SurveyService : ISurveyService
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ILogger<SurveyService> _logger;

    public SurveyService(ISurveyRepository surveyRepository, ILogger<SurveyService> logger)
    {
        _surveyRepository = surveyRepository;
        _logger = logger;
    }

    public async Task<bool> ExistsSurvey()
    {
        try
        {
            _logger.LogInformation("Delegating SurveyExistsToday to repository");
            return await _surveyRepository.SurveyExistsToday();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking if survey exists today");
            throw;
        }
    }

    public async Task<bool> SubmitSurvey(DateOnly date, List<Answer> answers)
    {
        try
        {
            _logger.LogInformation("Creating SurveySession entity with date: {Date}", date);
            var session = new SurveySession
            {
                Date = date
            };
            _logger.LogInformation("Entity is created -> Delegating SurveySubmit to repository with \n session date: {Date} \n answer count: {AnswerCount}", session.Date, answers.Count);
            return await _surveyRepository.SurveySubmit(session, answers);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting survey with \n session date: {Date} \n answer count: {AnswerCount}", date, answers.Count);
            throw;
        }
    }


}
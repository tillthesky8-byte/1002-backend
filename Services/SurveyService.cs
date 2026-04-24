using _1002_backend.Models;
using _1002_backend.Models.SpecialModels;
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
            _logger.LogInformation("\n ---START--- \n \n Delegating SurveyExistsToday to repository \n \n ---END--- \n");
            return await _surveyRepository.SurveyExistsToday();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error checking if survey exists today \n \n ---END--- \n");
            throw;
        }
    }

    public async Task<bool> SubmitSurvey(DateOnly date, List<BuildAnswer> answers)
    {
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Creating SurveySession entity with date: {Date} \n \n ---END--- \n", date);
            var session = new SurveySession
            {
                Date = date
            };

            var answerEntities = answers.Select(a => new Answer
            {
                QuestionId = a.QuestionId,
                Response = a.Response,
                Remark = a.Remark
            }).ToList();
            
            _logger.LogInformation("\n ---START--- \n \n Entity is created -> Delegating SurveySubmit to repository with \n session date: {Date} \n answer count: {AnswerCount} \n \n ---END--- \n", session.Date, answers.Count);
            return await _surveyRepository.SurveySubmit(session, answerEntities);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error submitting survey with \n session date: {Date} \n answer count: {AnswerCount} \n \n ---END--- \n", date, answers.Count);
            throw;
        }
    }


}
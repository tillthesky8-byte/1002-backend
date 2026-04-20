using _1002_backend.Dtos.Survey;
using _1002_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc; 

namespace _1002_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveyController : ControllerBase
{
    private readonly ISurveyService _surveyService;
    private readonly ILogger<SurveyController> _logger; 

    public SurveyController(ISurveyService surveyService, ILogger<SurveyController> logger)
    {
        _surveyService = surveyService;
        _logger = logger;
    }

    // POST: api/survey
    [HttpPost]
    public async Task<IActionResult> SubmitSurvey([FromBody] SubmitSurveyRequest request)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to submit survey with date: {Date} and answers: {Answers} \n \n ---END--- \n", request.Date, request.Answers);
        _logger.LogInformation("\n ---START--- \n \n Delegating {SubmitSurvey} to service ...  \n \n ---END--- \n", nameof(_surveyService.SubmitSurvey));
        var success = await _surveyService.SubmitSurvey(request.Date, request.Answers);
        if (!success) return BadRequest();
        return Ok();
    }  

    // GET: api/survey/existence
    [HttpGet("existence")]
    public async Task<IActionResult> CheckSurveyExistence() 
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to check survey existence for today \n \n ---END--- \n");
        _logger.LogInformation("\n ---START--- \n \n Delegating {ExistsSurvey} to service ... \n \n ---END--- \n", nameof(_surveyService.ExistsSurvey));
        var exists = await _surveyService.ExistsSurvey();
        return Ok(new SurveyExistenceResponse { Exists = exists });
    }
}
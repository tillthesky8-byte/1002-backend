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
        _logger.LogInformation("Received request to submit survey with date: {Date} and answers: {Answers}", request.Date, request.Answers);
        _logger.LogInformation("Delegating SubmitSurvey to service with date: {Date} and answers: {Answers}", request.Date, request.Answers);
        var success = await _surveyService.SubmitSurvey(request.Date, request.Answers);
        if (!success) return BadRequest();
        return Ok();
    }  

    // GET: api/survey/existence
    [HttpGet("existence")]
    public async Task<IActionResult> CheckSurveyExistence() 
    {
        _logger.LogInformation("Received request to check survey existence for today");
        _logger.LogInformation("Delegating ExistsSurvey to service");
        var exists = await _surveyService.ExistsSurvey();
        return Ok(new SurveyExistenceResponse { Exists = exists });
    }
}
using _1002_backend.Dtos.DreamEntries;
using _1002_backend.Services.Interfaces;
using _1002_backend.Models.PatchModels;

using Microsoft.AspNetCore.Mvc;
namespace _1002_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DreamEntriesController : ControllerBase
{
    private readonly IDreamEntryService _dreamEntryService;
    private readonly ILogger<DreamEntriesController> _logger;

    public DreamEntriesController(IDreamEntryService dreamEntryService, ILogger<DreamEntriesController> logger)
    {
        _dreamEntryService = dreamEntryService;
        _logger = logger;
    }

    // GET: api/dreamentries?pageNumber={pageNumber}&pageSize={pageSize}
    [HttpGet]
    public async Task<IActionResult> GetAllDreamEntries([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to get all dream entries \n pageNumber: {PageNumber}, pageSize: {PageSize} \n \n ---END--- \n", pageNumber, pageSize);
        // with middleware handling exceptions, we can just let the exception throw and be handled by middleware.
        _logger.LogInformation("\n ---START--- \n \n Delegating {GetAllDreamEntries} to service ... \n \n ---END--- \n", nameof(_dreamEntryService.GetAllDreamEntries));
        var entries = await _dreamEntryService.GetAllDreamEntries(pageNumber, pageSize);
        return Ok(entries);
    }

    // GET: api/dreamentries/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDreamEntryById(int id)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to get dream entry by id: {Id} \n \n ---END--- \n", id);
        _logger.LogInformation("\n ---START--- \n \n Delegating {GetDreamEntryById} to service ... \n \n ---END--- \n", nameof(_dreamEntryService.GetDreamEntryById));
        var entry = await _dreamEntryService.GetDreamEntryById(id);
        if (entry == null) return NotFound();
        return Ok(entry);
    }   

    // POST: api/dreamentries
    [HttpPost]
    public async Task<IActionResult> CreateDreamEntry([FromBody] CreateDreamEntryRequest request)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to create dream entry with title: \n {Title}, \n content: {Content}, \n date: {Date} \n \n ---END--- \n", request.Title, request.Content, request.Date);
        _logger.LogInformation("\n ---START--- \n \n Delegating {CreateDreamEntry} to service ... \n \n ---END--- \n", nameof(_dreamEntryService.CreateDreamEntry));
        var success = await _dreamEntryService.CreateDreamEntry(request.Title, request.Content, request.Date);
        if (!success) return BadRequest();
        return Ok();
    }

    // PUT: api/dreamentries/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDreamEntry(int id, [FromBody] UpdateDreamEntryRequest request)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to update dream entry with id: {Id}, title: {Title}, content: {Content}, date: {Date} \n \n ---END--- \n", id, request.Title, request.Content, request.Date);
        _logger.LogInformation("\n ---START--- \n \n Delegating {UpdateDreamEntry} to service ... \n \n ---END--- \n", nameof(_dreamEntryService.UpdateDreamEntry));
        var success = await _dreamEntryService.UpdateDreamEntry(request.Title, request.Content, request.Date, id);
        if (!success) return BadRequest();
        return Ok();
    }

    // DELETE: api/dreamentries/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDreamEntry(int id)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to delete dream entry with id: {Id} \n \n ---END--- \n", id);
        _logger.LogInformation("\n ---START--- \n \n Delegating {DeleteDreamEntry} to service ... \n \n ---END--- \n", nameof(_dreamEntryService.DeleteDreamEntry));
        var success = await _dreamEntryService.DeleteDreamEntry(id);
        if (!success) return BadRequest();
        return Ok();
    }
    // PATCH: api/dreamentries/{id}
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchDreamEntry(int id, [FromBody] PatchDreamEntryRequest request)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to patch dream entry with id: {Id}, title: {Title}, content: {Content}, date: {Date} \n \n ---END--- \n", id, request.Title, request.Content, request.Date);
        
        var patch = new DreamEntryPatch
        {
            Title = request.Title,
            Content = request.Content,
            Date = request.Date
        };

        _logger.LogInformation("\n ---START--- \n \n Delegating {PatchDreamEntry} to service ... \n \n ---END--- \n", nameof(_dreamEntryService.PatchDreamEntry));
        var success = await _dreamEntryService.PatchDreamEntry(patch, id);
        if (!success) return BadRequest();
        return Ok();
    }
}
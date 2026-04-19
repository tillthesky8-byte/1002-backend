using _1002_backend.Dtos.DreamEntries;
using _1002_backend.Services.Interfaces;

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
        _logger.LogInformation("Received request to get all dream entries \n pageNumber: {PageNumber}, pageSize: {PageSize}", pageNumber, pageSize);
        // with middleware handling exceptions, we can just let the exception throw and be handled by middleware.
        _logger.LogInformation("Delegating GetAllDreamEntries to service with \n pageNumber: {PageNumber}, pageSize: {PageSize}", pageNumber, pageSize);
        var entries = await _dreamEntryService.GetAllDreamEntries(pageNumber, pageSize);
        return Ok(entries);
    }

    // GET: api/dreamentries/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDreamEntryById(int id)
    {
        _logger.LogInformation("Received request to get dream entry by id: {Id}", id);
        _logger.LogInformation("Delegating GetDreamEntryById to service with id: {Id}", id);
        var entry = await _dreamEntryService.GetDreamEntryById(id);
        if (entry == null) return NotFound();
        return Ok(entry);
    }   

    // POST: api/dreamentries
    [HttpPost]
    public async Task<IActionResult> CreateDreamEntry([FromBody] CreateDreamEntryRequest request)
    {
        _logger.LogInformation("Received request to create dream entry with title: \n {Title}, \n content: {Content}, \n date: {Date}", request.Title, request.Content, request.Date);
        _logger.LogInformation("Delegating CreateDreamEntry to service with title: \n {Title}, \n content: {Content}, \n date: {Date}", request.Title, request.Content, request.Date);
        var success = await _dreamEntryService.CreateDreamEntry(request.Title, request.Content, request.Date);
        if (!success) return BadRequest();
        return Ok();
    }

    // PUT: api/dreamentries/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDreamEntry(int id, [FromBody] UpdateDreamEntryRequest request)
    {
        _logger.LogInformation("Received request to update dream entry with id: {Id}, title: {Title}, content: {Content}, date: {Date}", id, request.Title, request.Content, request.Date);
        _logger.LogInformation("Delegating UpdateDreamEntry to service with id: {Id}, title: {Title}, content: {Content}, date: {Date}", id, request.Title, request.Content, request.Date);
        var success = await _dreamEntryService.UpdateDreamEntry(request.Title, request.Content, request.Date, id);
        if (!success) return BadRequest();
        return Ok();
    }

    // DELETE: api/dreamentries/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDreamEntry(int id)
    {
        _logger.LogInformation("Received request to delete dream entry with id: {Id}", id);
        _logger.LogInformation("Delegating DeleteDreamEntry to service with id: {Id}", id);
        var success = await _dreamEntryService.DeleteDreamEntry(id);
        if (!success) return BadRequest();
        return Ok();
    }
}
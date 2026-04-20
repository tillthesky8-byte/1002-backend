using _1002_backend.Dtos.Todos;
using _1002_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace _1002_backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodoController> _logger; 
    public TodoController(ITodoService todoService, ILogger<TodoController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }
    
    // POST: api/todo
    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoRequest request)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to create todo with title: {Text} \n \n ---END--- \n", request.Text);
        _logger.LogInformation("\n ---START--- \n \n Delegating {CreateTodo} to service ...  \n \n ---END--- \n", nameof(_todoService.CreateTodo));
        var success = await _todoService.CreateTodo(request.Text, request.DueAt, request.TimeFrameId);
        if (!success) return BadRequest();
        return Ok();
    }

    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoRequest request)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to update todo with id: {Id}, title: {Text}, dueAt: {DueAt}, finishedAt: {FinishedAt}, statusId: {StatusId}, timeFrameId: {TimeFrameId} \n \n ---END--- \n", id, request.Text, request.DueAt, request.FinishedAt, request.StatusId, request.TimeFrameId);
        _logger.LogInformation("\n ---START--- \n \n Delegating {UpdateTodo} to service ...  \n \n ---END--- \n", nameof(_todoService.UpdateTodo));
        var success = await _todoService.UpdateTodo(request.Text, request.DueAt, request.FinishedAt, request.StatusId, request.TimeFrameId, id);
        if (!success) return BadRequest();
        return Ok();
    }

    // GET: api/todo?pageNumber={pageNumber}&pageSize={pageSize}
    [HttpGet]
    public async Task<IActionResult> GetAllTodos([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to get all todos with pageNumber: {PageNumber} and pageSize: {PageSize} \n \n ---END--- \n", pageNumber, pageSize);
        _logger.LogInformation("\n ---START--- \n \n Delegating {GetAllTodos} to service ... \n \n ---END--- \n", nameof(_todoService.GetAllTodos));
        var todos = await _todoService.GetAllTodos(pageNumber, pageSize);
        return Ok(todos);
    }

    // GET: api/todo/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodoById(int id)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to get todo by id: {Id} \n \n ---END--- \n", id);
        _logger.LogInformation("\n ---START--- \n \n Delegating {GetTodoById} to service ... \n \n ---END--- \n", nameof(_todoService.GetTodoById));
        var todo = await _todoService.GetTodoById(id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to delete todo with id: {Id} \n \n ---END--- \n", id);
        _logger.LogInformation("\n ---START--- \n \n Delegating {DeleteTodo} to service ... \n \n ---END--- \n", nameof(_todoService.DeleteTodo));
        var success = await _todoService.DeleteTodo(id);
        if (!success) return BadRequest();
        return Ok();
    }  

    // POST: api/todo/{id}/complete
    [HttpPost("{id}/complete")]
    public async Task<IActionResult> MarkTodoAsCompleted(int id)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to mark todo as completed with id: {Id} \n \n ---END--- \n", id);
        _logger.LogInformation("\n ---START--- \n \n Delegating {MarkTodoAsFinished} to service ... \n \n ---END--- \n", nameof(_todoService.MarkTodoAsFinished));
        var success = await _todoService.MarkTodoAsFinished(id, false);
        if (!success) return BadRequest();
        return Ok();
    }

    // POST: api/todo/{id}/fail
    [HttpPost("{id}/fail")]
    public async Task<IActionResult> MarkTodoAsFailed(int id)
    {
        _logger.LogInformation("\n ---START--- \n \n Received request to mark todo as failed with id: {Id} \n \n ---END--- \n", id);
        _logger.LogInformation("\n ---START--- \n \n Delegating {MarkTodoAsFinished} to service ... \n \n ---END--- \n", nameof(_todoService.MarkTodoAsFinished));
        var success = await _todoService.MarkTodoAsFinished(id, true);
        if (!success) return BadRequest();
        return Ok();
    }
}
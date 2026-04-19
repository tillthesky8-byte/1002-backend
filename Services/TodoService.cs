using _1002_backend.Models;
using _1002_backend.Repositories.Interfaces;
using _1002_backend.Services.Interfaces;

namespace _1002_backend.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    private readonly ILogger<TodoService> _logger;

    public TodoService(ITodoRepository todoRepository, ILogger<TodoService> logger)
    {
        _todoRepository = todoRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Todo>> GetAllTodos(int pageNumber, int pageSize)
    {
        if (pageNumber < 1) pageNumber = 1; _logger.LogWarning("Page number less than 1, defaulting to 1");
        if (pageSize < 1) pageSize = 10; _logger.LogWarning("Page size less than 1, defaulting to 10");
        try
        {
            _logger.LogInformation("Delegating GetAllTodos to repository with \n pageNumber: {PageNumber}, pageSize: {PageSize}", pageNumber, pageSize);
            return await _todoRepository.GetAllTodos(pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAllTodos with \n pageNumber: {PageNumber}, pageSize: {PageSize}", pageNumber, pageSize);
            throw;
        }
    }

    public async Task<Todo?> GetTodoById(int id)
    {
        try
        {
            _logger.LogInformation("Delegating GetTodoById to repository with id: {Id}", id);
            return await _todoRepository.GetTodoById(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetTodoById with id: {Id}", id);
            throw;
        }
    }

    public async Task<bool> CreateTodo(string text, int? dueAt, int timeFrameId)
    {
        try
        {
            _logger.LogInformation("Creating Todo entity with text: \n {Text}, \n dueAt: {DueAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId}", text, dueAt, 1, timeFrameId);
            var todo = new Todo
            {
                Title = text,
                DueAt = dueAt,
                StatusId = 1,
                TimeFrameId = timeFrameId
            };

            _logger.LogInformation("Entity is created -> Delegating CreateTodo to repository with text: \n {Text}, \n dueAt: {DueAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId}", text, dueAt, 1, timeFrameId);
            return await _todoRepository.CreateTodo(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateTodo with text: \n {Text}, \n dueAt: {DueAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId}", text, dueAt, 1, timeFrameId);
            throw;
        }
    }

    public async Task<bool> UpdateTodo(string text, int? dueAt, int? finishedAt, int statusId, int timeFrameId, int id)
    {
        try
        {
            _logger.LogInformation("Creating Todo entity with id: {Id}, text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId}", id, text, dueAt, finishedAt, statusId, timeFrameId);
            var todo = new Todo
            {
                Id = id,
                Title = text,
                DueAt = dueAt,
                FinishedAt = finishedAt,
                StatusId = statusId,
                TimeFrameId = timeFrameId
            };  

            _logger.LogInformation("Entity is created -> Delegating UpdateTodo to repository with id: {Id}, text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId}", id, text, dueAt, finishedAt, statusId, timeFrameId);
            return await _todoRepository.UpdateTodo(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateTodo with id: {Id}, text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId}", id, text, dueAt, finishedAt, statusId, timeFrameId);
            throw;
        }
    }

    public async Task<bool> DeleteTodo(int id)
    {
        try
        {
            _logger.LogInformation("Delegating DeleteTodo to repository with id: {Id}", id);
            return await _todoRepository.DeleteTodo(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteTodo with id: {Id}", id);
            throw;
        }
    }

    public async Task<bool> MarkTodoAsFinished(int id, bool isFailed)
    {
        //method need patch operation in repository and database, so it is not implemented yet
        try
        {            
            _logger.LogInformation("Delegating MarkTodoAsFinished to repository with id: {Id}, isFailed: {IsFailed}", id, isFailed);
            return true; //await _todoRepository.MarkTodoAsFinished(id, isFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MarkTodoAsFinished with id: {Id}, isFailed: {IsFailed}", id, isFailed);
            throw;
        }   
    }
}
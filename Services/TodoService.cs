using _1002_backend.Models;
using _1002_backend.Models.PatchModels;
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
        if (pageNumber < 1) pageNumber = 1; _logger.LogWarning("\n ---START--- \n \n Page number less than 1, defaulting to 1 \n \n ---END--- \n");
        if (pageSize < 1) pageSize = 10; _logger.LogWarning("\n ---START--- \n \n Page size less than 1, defaulting to 10 \n \n ---END--- \n");
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Delegating GetAllTodos to repository with \n pageNumber: {PageNumber}, pageSize: {PageSize} \n \n ---END--- \n", pageNumber, pageSize);
            return await _todoRepository.GetAllTodos(pageNumber, pageSize);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in GetAllTodos with \n pageNumber: {PageNumber}, pageSize: {PageSize} \n \n ---END--- \n", pageNumber, pageSize);
            throw;
        }
    }

    public async Task<Todo?> GetTodoById(int id)
    {
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Delegating GetTodoById to repository with id: {Id} \n \n ---END--- \n", id);
            return await _todoRepository.GetTodoById(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in GetTodoById with id: {Id} \n \n ---END--- \n", id);
            throw;
        }
    }

    public async Task<bool> CreateTodo(string text, int? dueAt, int timeFrameId)
    {
        if (timeFrameId < 1) timeFrameId = 1; _logger.LogWarning("\n ---START--- \n \n TimeFrameId less than 1, defaulting to 1 \n \n ---END--- \n");
        if (timeFrameId > 3) timeFrameId = 3; _logger.LogWarning("\n ---START--- \n \n TimeFrameId greater than 3, defaulting to 3 \n \n ---END--- \n");
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Creating Todo entity with text: \n {Text}, \n dueAt: {DueAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", text, dueAt, 1, timeFrameId);
            var todo = new Todo
            {
                Text = text,
                DueAt = dueAt,
                StatusId = 1,
                TimeFrameId = timeFrameId
            };

            _logger.LogInformation("\n ---START--- \n \n Entity is created -> Delegating CreateTodo to repository with text: \n {Text}, \n dueAt: {DueAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", text, dueAt, 1, timeFrameId);
            return await _todoRepository.CreateTodo(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in CreateTodo with text: \n {Text}, \n dueAt: {DueAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", text, dueAt, 1, timeFrameId);
            throw;
        }
    }

    public async Task<bool> UpdateTodo(string text, int? dueAt, int? finishedAt, int statusId, int timeFrameId, int id)
    {
        if (timeFrameId < 1) timeFrameId = 1; _logger.LogWarning("\n ---START--- \n \n TimeFrameId less than 1, defaulting to 1 \n \n ---END--- \n");
        if (timeFrameId > 3) timeFrameId = 3; _logger.LogWarning("\n ---START--- \n \n TimeFrameId greater than 3, defaulting to 3 \n \n ---END--- \n");
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Creating Todo entity with id: {Id}, text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", id, text, dueAt, finishedAt, statusId, timeFrameId);
            var todo = new Todo
            {
                Id = id,
                Text = text,
                DueAt = dueAt,
                FinishedAt = finishedAt,
                StatusId = statusId,
                TimeFrameId = timeFrameId
            };  

            _logger.LogInformation("\n ---START--- \n \n Entity is created -> Delegating UpdateTodo to repository with id: {Id}, text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", id, text, dueAt, finishedAt, statusId, timeFrameId);
            return await _todoRepository.UpdateTodo(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in UpdateTodo with id: {Id}, text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", id, text, dueAt, finishedAt, statusId, timeFrameId);
            throw;
        }
    }

    public async Task<bool> DeleteTodo(int id)
    {
        try
        {
            _logger.LogInformation("\n ---START--- \n \n Delegating DeleteTodo to repository with id: {Id} \n \n ---END--- \n", id);
            return await _todoRepository.DeleteTodo(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in DeleteTodo with id: {Id} \n \n ---END--- \n", id);
            throw;
        }
    }

    public async Task<bool> MarkTodoAsFinished(int id, bool isFailed)
    {
        //method need patch operation in repository and database, so it is not implemented yet
        try
        {            
            _logger.LogInformation("\n ---START--- \n \n Delegating MarkTodoAsFinished to repository with id: {Id}, isFailed: {IsFailed} \n \n ---END--- \n", id, isFailed);
            return true; //await _todoRepository.MarkTodoAsFinished(id, isFailed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in MarkTodoAsFinished with id: {Id}, isFailed: {IsFailed} \n \n ---END--- \n", id, isFailed);
            throw;
        }   
    }

    public async Task<bool> PatchTodo(TodoPatch patch, int id)
    {
        try
        {
            var todoToPatch = await _todoRepository.GetTodoById(id);
            if (todoToPatch == null)
            {
                _logger.LogWarning("\n ---START--- \n \n No todo found with ID {Id} for patching \n \n ---END--- \n", id);
                return false;
            }
            var updatedTodo = new Todo
            {
                Id = id,
                Text = patch.Text ?? todoToPatch.Text,
                DueAt = patch.DueAt ?? todoToPatch.DueAt,
                FinishedAt = patch.FinishedAt ?? todoToPatch.FinishedAt,
                StatusId = patch.StatusId ?? todoToPatch.StatusId,
                TimeFrameId = patch.TimeFrameId ?? todoToPatch.TimeFrameId
            };

            return await _todoRepository.UpdateTodo(updatedTodo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error in PatchTodo with id: {Id} and patch data: text: \n {Text}, \n dueAt: {DueAt}, \n finishedAt: {FinishedAt}, \n statusId: {StatusId}, \n timeFrameId: {TimeFrameId} \n \n ---END--- \n", id, patch.Text, patch.DueAt, patch.FinishedAt, patch.StatusId, patch.TimeFrameId);
            throw;
        }
    }
}   
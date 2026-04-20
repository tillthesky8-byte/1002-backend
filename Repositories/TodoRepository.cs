using _1002_backend.Models;
using _1002_backend.Infrastructure.Data;
using _1002_backend.Repositories.Interfaces;
using Dapper;

namespace _1002_backend.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<TodoRepository> _logger;

    public TodoRepository(IDbConnectionFactory dbConnectionFactory, ILogger<TodoRepository> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }

    // CRUD operations for Todo
    public async Task<IEnumerable<Todo>> GetAllTodos(int pageNumber, int pageSize)
    {
        // Basic input validation for pagination parameters.
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var offset = (pageNumber - 1) * pageSize;
            const string query = @"SELECT Id, Text, CreatedAt, DueAt, FinishedAt, StatusId, TimeFrameId
                                   FROM Todos
                                   ORDER BY CreatedAt DESC
                                   LIMIT @PageSize OFFSET @Offset";
            var result = (await connection.QueryAsync<Todo>(query, new { PageSize = pageSize, Offset = offset })).AsList();
            _logger.LogInformation("\n ---START--- \n \n Fetched {Count} todos for page {PageNumber} with page size {PageSize} \n \n ---END--- \n", result.Count, pageNumber, pageSize);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error fetching todos \n \n ---END--- \n");
            throw;
        }
    }

    public async Task<Todo?> GetTodoById(int id)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"SELECT Id, Text, CreatedAt, DueAt, FinishedAt, StatusId, TimeFrameId
                                   FROM Todos
                                   WHERE Id = @Id";
            var result = await connection.QuerySingleOrDefaultAsync<Todo>(query, new { Id = id });

            if (result != null) _logger.LogInformation("\n ---START--- \n \n Fetched todo with ID {Id} \n \n ---END--- \n", id);
            else _logger.LogWarning("\n ---START--- \n \n No todo found with ID {Id} \n \n ---END--- \n", id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error fetching todo with ID {Id} \n \n ---END--- \n", id);
            throw;
        }
    }

    public async Task<bool> CreateTodo(Todo todo)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"INSERT INTO Todos (Text, CreatedAt, DueAt, FinishedAt, StatusId, TimeFrameId)
                                   VALUES (@Text, @CreatedAt, @DueAt, @FinishedAt, @StatusId, @TimeFrameId);
                                   SELECT last_insert_rowid();";
                                   // Note: Text/Title mapping, rename in the future.
            var newId = await connection.ExecuteScalarAsync<int>(query, todo);
            _logger.LogInformation("\n ---START--- \n \n Created new todo with ID {Id} \n \n ---END--- \n", newId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error creating new todo \n \n ---END--- \n");
            throw;
        }
    }

    public async Task<bool> UpdateTodo(Todo todo)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"UPDATE Todos
                                   SET Text = @Text,
                                       CreatedAt = @CreatedAt,
                                       DueAt = @DueAt,
                                       FinishedAt = @FinishedAt,
                                       StatusId = @StatusId,
                                       TimeFrameId = @TimeFrameId
                                   WHERE Id = @Id";
                                    // Note: Text/Title mapping, rename in the future.
            var rowsAffected = await connection.ExecuteAsync(query, todo);
            if (rowsAffected > 0) _logger.LogInformation("\n ---START--- \n \n Updated todo with ID {Id} \n \n ---END--- \n", todo.Id);
            else _logger.LogWarning("\n ---START--- \n \n No todo found to update with ID {Id} \n \n ---END--- \n", todo.Id);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error updating todo with ID {Id} \n \n ---END--- \n", todo.Id);
            throw;
        }
    }

    public async Task<bool> DeleteTodo(int id)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"DELETE FROM Todos WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            if (rowsAffected > 0) _logger.LogInformation("\n ---START--- \n \n Deleted todo with ID {Id} \n \n ---END--- \n", id);
            else _logger.LogWarning("\n ---START--- \n \n No todo found to delete with ID {Id} \n \n ---END--- \n", id);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "\n ---START--- \n \n Error deleting todo with ID {Id} \n \n ---END--- \n", id);
            throw;
        }
    }
}

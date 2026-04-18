using _1002_backend.Models;
using _1002_backend.Infrastructure.Data;
using _1002_backend.Repositories.Interfaces;
using Dapper;

namespace _1002_backend.Repositories;

public class DreamEntryRepository : IDreamEntryRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;
    private readonly ILogger<DreamEntryRepository> _logger;

    public DreamEntryRepository(IDbConnectionFactory dbConnectionFactory, ILogger<DreamEntryRepository> logger)
    {
        _dbConnectionFactory = dbConnectionFactory;
        _logger = logger;
    }

    public async Task<IEnumerable<DreamEntry>> GetAllDreamEntries(int pageNumber, int pageSize)
    {
        // basic input validation
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            var offset = (pageNumber - 1) * pageSize;
            const string query = @"SELECT Id, Title, Description, Date
                                   FROM DreamEntries
                                   ORDER BY Date DESC
                                   LIMIT @PageSize OFFSET @Offset";
            var result = (await connection.QueryAsync<DreamEntry>(query, new { PageSize = pageSize, Offset = offset })).AsList();
            _logger.LogInformation("Fetched {Count} dream entries for page {PageNumber} with page size {PageSize}", result.Count, pageNumber, pageSize);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dream entries");
            throw; // Rethrow the exception to be handled by middleware
        }

    }

    public async Task<DreamEntry?> GetDreamEntryById(int id)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"SELECT Id, Title, Description, Date
                                   FROM DreamEntries
                                   WHERE Id = @Id";
            var result = await connection.QuerySingleOrDefaultAsync<DreamEntry>(query, new { Id = id });
            
            if (result != null) _logger.LogInformation("Fetched dream entry with ID {Id}", id);
            else _logger.LogWarning("No dream entry found with ID {Id}", id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dream entry with ID {Id}", id);
            throw; // Rethrow the exception to be handled by middleware
        }
    }

    public async Task<bool> CreateDreamEntry(DreamEntry entry)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"INSERT INTO DreamEntries (Title, Description, Date)
                                   VALUES (@Title, @Description, @Date);
                                   SELECT last_insert_rowid();";
            var newId = await connection.ExecuteScalarAsync<int>(query, entry);
            _logger.LogInformation("Created new dream entry with ID {Id}", newId);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating new dream entry");
            throw; // Rethrow the exception to be handled by middleware
        }
    }

    public async Task<bool> UpdateDreamEntry(DreamEntry entry)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"UPDATE DreamEntries
                                   SET Title = @Title, Description = @Description, Date = @Date
                                   WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, entry);
            if (rowsAffected > 0) _logger.LogInformation("Updated dream entry with ID {Id}", entry.Id);
            else _logger.LogWarning("No dream entry found to update with ID {Id}", entry.Id);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating dream entry with ID {Id}", entry.Id);
            throw; // Rethrow the exception to be handled by middleware
        }
    }

    public async Task<bool> DeleteDreamEntry(int id)
    {
        try
        {
            using var connection = _dbConnectionFactory.CreateConnection();
            const string query = @"DELETE FROM DreamEntries WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            if (rowsAffected > 0) _logger.LogInformation("Deleted dream entry with ID {Id}", id);
            else _logger.LogWarning("No dream entry found to delete with ID {Id}", id);
            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting dream entry with ID {Id}", id);
            throw; // Rethrow the exception to be handled by middleware
        }
    }
}
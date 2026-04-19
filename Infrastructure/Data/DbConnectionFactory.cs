using System.Data;
using Microsoft.Data.Sqlite;

namespace _1002_backend.Infrastructure.Data;

public sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");
    }
    
    // Note for the future: Switch IDbConnection to DbConnection for async support.
    public IDbConnection CreateConnection()
    {
        return new SqliteConnection(_connectionString);
    }
}

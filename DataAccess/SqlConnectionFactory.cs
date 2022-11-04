using Microsoft.Data.SqlClient;

namespace DataAccess;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString = "";

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
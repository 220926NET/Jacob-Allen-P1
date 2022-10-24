using Microsoft.Data.SqlClient;

namespace DataAccess;

public interface ISqlConnectionFactory
{
    SqlConnection GetConnection();
}
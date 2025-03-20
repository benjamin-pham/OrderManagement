using Microsoft.Data.SqlClient;
using System.Data;

namespace OrderManagement.Infrastructure.Data;

internal class SqlConnectionFactory
{
    private readonly string _connectionString;
    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}

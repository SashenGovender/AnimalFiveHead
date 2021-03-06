using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Common.DatabaseDapperAccess.ConnectionProviders
{
  public class SqlConnectionProvider : IDatabaseConnectionProvider
  {
    private readonly IConfiguration _configuration;

    public SqlConnectionProvider(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<IDbConnection> OpenConnectionAsync(string databaseName, CancellationToken token)
    {
      var connectionString = _configuration.GetConnectionString(databaseName);

      var connection = new SqlConnection(connectionString);
      await connection.OpenAsync(token);

      return connection;
    }

  }
}

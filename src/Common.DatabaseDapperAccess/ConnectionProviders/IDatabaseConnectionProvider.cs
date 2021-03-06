using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DatabaseDapperAccess.ConnectionProviders
{
  public interface IDatabaseConnectionProvider
  {
    Task<IDbConnection> OpenConnectionAsync(string databaseName, CancellationToken token);
  }
}

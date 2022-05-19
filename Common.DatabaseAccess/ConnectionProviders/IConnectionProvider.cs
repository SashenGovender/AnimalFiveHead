using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DatabaseAccess.ConnectionProviders
{
  public interface IConnectionProvider
  {
    Task<IDbConnection> OpenConnectionAsync(string databaseName, CancellationToken token);
  }
}

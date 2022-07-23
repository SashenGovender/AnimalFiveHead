using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Common.DatabaseDapperAccess
{
  public interface IDatabaseDapperAccess
  {
    public Task<int> ExecuteAsync(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure);
    public Task<IEnumerable<T>> QueryAsync<T>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure);
    public Task<T> QueryFirstAsync<T>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure);
    public Task<T> QueryFirstOrDefaultAsync<T>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure);
    public Task<(IEnumerable<T>, IEnumerable<TU>)> QueryMultipleAsync<T, TU>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure);
  }
}

using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using Common.DatabaseAccess.ConnectionProviders;

namespace Common.DatabaseAccess
{
  public class DapperDatabaseAccess : IDatabaseAccess
  {
    private readonly IDatabaseConnectionProvider _connectionProvider;

    public DapperDatabaseAccess(IDatabaseConnectionProvider connectionProvider)
    {
      _connectionProvider = connectionProvider;
    }

    public async Task<int> ExecuteAsync(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure)
    {
      using var connection = await _connectionProvider.OpenConnectionAsync(databaseName, token);

      return await connection.ExecuteAsync(query, parameters, transaction: transaction, commandTimeout: timeout, commandType: commandType);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure)
    {
      using var connection = await _connectionProvider.OpenConnectionAsync(databaseName, token);

      return await connection.QueryAsync<T>(query, parameters, transaction: transaction, commandTimeout: timeout, commandType: commandType);
    }

    public async Task<T> QueryFirstAsync<T>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure)
    {
      using var connection = await _connectionProvider.OpenConnectionAsync(databaseName, token);

      return await connection.QueryFirstAsync<T>(query, parameters, transaction: transaction, commandTimeout: timeout, commandType: commandType);
    }

    public async Task<T> QueryFirstOrDefaultAsync<T>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure)
    {
      using var connection = await _connectionProvider.OpenConnectionAsync(databaseName, token);

      return await connection.QueryFirstOrDefaultAsync<T>(query, parameters, transaction: transaction, commandTimeout: timeout, commandType: commandType);
    }
  }
}

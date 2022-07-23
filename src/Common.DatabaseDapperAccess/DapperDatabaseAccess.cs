using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
using System.Threading;
using Common.DatabaseDapperAccess.ConnectionProviders;

namespace Common.DatabaseDapperAccess
{
  //https://github.com/DapperLib/Dapper
  public class DapperDatabaseAccess : IDatabaseDapperAccess
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

    // https://www.red-gate.com/simple-talk/development/dotnet-development/a-practical-guide-to-dapper/
    // https://code-maze.com/using-dapper-with-asp-net-core-web-api/
    // QueryMultiple returns a gridReader which implements IDisposible. Hence the using. https://github.com/DapperLib/Dapper/blob/main/Dapper/SqlMapper.GridReader.cs
    public async Task<(IEnumerable<T>, IEnumerable<TU>)> QueryMultipleAsync<T, TU>(string databaseName, string query, CancellationToken token, object? parameters = null, IDbTransaction? transaction = null, int? timeout = 30, CommandType commandType = CommandType.StoredProcedure)
    {
      using var connection = await _connectionProvider.OpenConnectionAsync(databaseName, token);

      using var gridReaderData = await connection.QueryMultipleAsync(query, parameters, transaction: transaction, commandTimeout: timeout, commandType: commandType);
      var firstDataRead = gridReaderData.Read<T>();
      var secondDataRead = gridReaderData.Read<TU>();

      return (firstDataRead, secondDataRead);
    }
  }
}

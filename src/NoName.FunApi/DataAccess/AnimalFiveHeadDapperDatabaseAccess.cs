using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Common.DatabaseDapperAccess;
using Common.DatabaseDapperAccess.Exceptions;
using Common.Models.Domain.AnimalFiveHead;
using Dapper;
using Microsoft.Extensions.Logging;

namespace NoName.FunApi.DataAccess
{
  public class AnimalFiveHeadDapperDatabaseAccess : IAnimalFiveDatabaseAccess
  {
    private readonly IDatabaseDapperAccess _databaseAccess;
    private readonly ILogger<AnimalFiveHeadDapperDatabaseAccess> _logger;

    private const string DatabaseName = "AnimalFiveHead";
    private const string UpsertPlayerSessionProc = "dbo.pr_UpsertPlayerSession";
    private const string GetGameSessionProc = "dbo.pr_GetGameSession";
    private const string CompleteGameSessionProc = "dbo.pr_CompleteGameSession";
    private const string GameSessionExistAndActiveProc = "dbo.pr_GameSessionExistAndActive";

    public AnimalFiveHeadDapperDatabaseAccess(IDatabaseDapperAccess dataAccess, ILogger<AnimalFiveHeadDapperDatabaseAccess> logger)
    {
      _databaseAccess = dataAccess;
      _logger = logger;
    }

    public async Task UpsertPlayerSessionInformationAsync(AnimalFivePlayerSaveSessionData newPlayerData, CancellationToken token)
    {
      var parameters = new DynamicParameters();
      parameters.Add("SessionId", newPlayerData.SessionId);
      parameters.Add("PlayerId", newPlayerData.PlayerId);
      parameters.Add("Score", newPlayerData.Score);
      parameters.Add("Cards", newPlayerData.Cards);
      parameters.Add("CardIds", newPlayerData.CardIds);
      parameters.Add("GameResult", newPlayerData.Result.ToString());

      try
      {
        await _databaseAccess.ExecuteAsync(DatabaseName, UpsertPlayerSessionProc, token, parameters, commandType: CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to upsert player session information with error message {ErrorMessage}", ex.Message);
        throw new DatabaseException("UpsertPlayerSession");
      }
    }


    public async Task<IEnumerable<AnimalFivePlayerGetSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken token)
    {
      var parameters = new DynamicParameters();
      parameters.Add("SessionId", sessionId.ToString());

      try
      {
        var data = await _databaseAccess.QueryAsync<AnimalFivePlayerGetSessionData>(DatabaseName, GetGameSessionProc, token, parameters, commandType: CommandType.StoredProcedure);
        return data;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to get session using id {SessionId}. Error message {ErrorMessage}", sessionId, ex.Message);
        throw new DatabaseException("GetBySessionId");
      }
    }

    public async Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken token)
    {
      var parameters = new DynamicParameters();
      parameters.Add("SessionId", gameSessionId.ToString());

      try
      {
        await _databaseAccess.ExecuteAsync(DatabaseName, CompleteGameSessionProc, token, parameters, commandType: CommandType.StoredProcedure);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to complete game session with error message {ErrorMessage}", ex.Message);
        throw new DatabaseException("CompleteGameSession");
      }
    }

    public async Task<bool> GameSessionExistsAndActiveAsync(Guid gameSessionId, CancellationToken token)
    {
      var parameters = new DynamicParameters();
      parameters.Add("SessionId", gameSessionId.ToString());

      try
      {
        var exists = await _databaseAccess.QueryFirstAsync<int>(DatabaseName, GameSessionExistAndActiveProc, token, parameters, commandType: CommandType.StoredProcedure);
        return exists == 1;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to complete game session with error message {ErrorMessage}", ex.Message);
        throw new DatabaseException("GameSessionExistsAndActive");
      }
    }
  }
}

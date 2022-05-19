using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Common.DatabaseAccess;
using Common.DatabaseAccess.Exceptions;
using Dapper;
using Microsoft.Extensions.Logging;
using NoName.FunApi.Models.AnimalFive.Dto;

namespace NoName.FunApi.DataAccess
{
  public class AnimalFiveDatabaseAccess : IAnimalFiveDatabaseAccess
  {
    private readonly IDatabaseAccess _databaseAccess;
    private readonly ILogger<AnimalFiveDatabaseAccess> _logger;

    private const string DatabaseName = "AnimalFiveHead";
    private const string UpsertPlayerSessionProc = "dbo.pr_UpsertPlayerSession";
    private const string GetAllPlayerSessionProc = "dbo.pr_GetAllPlayerSessions";
    private const string GetAllPlayerSpecificSessionProc = "dbo.pr_GetAllPlayerSpecificSessions";
    private const string CompleteGameSessionProc = "dbo.pr_CompleteGameSession";

    public AnimalFiveDatabaseAccess(IDatabaseAccess dataAccess, ILogger<AnimalFiveDatabaseAccess> logger)
    {
      _databaseAccess = dataAccess;
      _logger = logger;
    }

    public async Task UpsertPlayerSessionInformationAsync(AnimalFivePlayerSessionData newPlayerData, CancellationToken token)
    {
      var parameters = new DynamicParameters();
      parameters.Add("SessionId", newPlayerData.SessionId);
      parameters.Add("PlayerId", newPlayerData.PlayerId);
      parameters.Add("Score", newPlayerData.Score);
      parameters.Add("PlayerCards", newPlayerData.PlayerCards);

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

    public async Task<IEnumerable<AnimalFivePlayerSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken token)
    {
      var parameters = new DynamicParameters();
      parameters.Add("SessionId", sessionId.ToString());

      try
      {
        var data = await _databaseAccess.QueryAsync<AnimalFivePlayerSessionData>(DatabaseName, GetAllPlayerSpecificSessionProc, token, parameters, commandType: CommandType.StoredProcedure);
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

    public async Task<IEnumerable<AnimalFivePlayerSessionData>> GetAllAsync(CancellationToken token)
    {
      try
      {
        var data = await _databaseAccess.QueryAsync<AnimalFivePlayerSessionData>(DatabaseName, GetAllPlayerSessionProc, token, commandType: CommandType.StoredProcedure);
        return data;
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Failed to get all session information with error message {ErrorMessage}", ex.Message);
        throw new DatabaseException("GetAll");
      }
    }
  }
}

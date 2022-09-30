using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Domain.AnimalFiveHead;
using Database.Entity.AnimalFiveHead;
using Database.Entity.AnimalFiveHead.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NoName.FunApi.DataAccess
{
  public class AnimalFiveHeadEFCoreDatabaseAccess : IAnimalFiveHeadDatabaseAccess
  {
    private readonly ILogger<AnimalFiveHeadEFCoreDatabaseAccess> _logger;
    private readonly AnimalFiveHeadContext _animalFiveHeadContext;

    public AnimalFiveHeadEFCoreDatabaseAccess(ILogger<AnimalFiveHeadEFCoreDatabaseAccess> logger, AnimalFiveHeadContext animalFiveHeadContext)
    {
      _logger = logger;
      _animalFiveHeadContext = animalFiveHeadContext;
    }

    public async Task UpsertPlayerSessionInformationAsync(AnimalFiveHeadPlayerSaveSessionData newPlayerData, CancellationToken cancellationToken)
    {

      var playerSessionEntity = await _animalFiveHeadContext.PlayerSessionInformations.FirstOrDefaultAsync(x => x.SessionId == newPlayerData.SessionId && x.PlayerId == newPlayerData.PlayerId, cancellationToken);
      if (playerSessionEntity == null)
      {
        // Add the new entry
        var playerSession = new PlayerSessionInformation
        {
          SessionId = newPlayerData.SessionId,
          PlayerId = newPlayerData.PlayerId,
          Score = newPlayerData.Score,
          Cards = newPlayerData.Cards,
          CardIds = newPlayerData.CardIds,
          GameSession = "Active",
          GameResult = newPlayerData.Result.ToString(),
          DateTimeAdded = DateTime.Now,
        };
        _animalFiveHeadContext.PlayerSessionInformations.Add(playerSession);

        var outcome = await _animalFiveHeadContext.SaveChangesAsync(cancellationToken);
        if (outcome < 0)
        {
          _logger.LogWarning("Error Adding Player Session Information");
        }
      }
      else
      {
        playerSessionEntity.Score = newPlayerData.Score;
        playerSessionEntity.Cards = newPlayerData.Cards;
        playerSessionEntity.CardIds = newPlayerData.CardIds;
        playerSessionEntity.GameResult = newPlayerData.Result.ToString();
        playerSessionEntity.DateTimeUpdated = DateTime.Now;

        var outcome1 = _animalFiveHeadContext.PlayerSessionInformations.Update(playerSessionEntity);
        var outcome2 = await _animalFiveHeadContext.SaveChangesAsync(cancellationToken);
        if (outcome2 < 0)
        {
          _logger.LogWarning("Error Updating Player Session Information");
        }
      }
    }

    public async Task<IEnumerable<AnimalFiveHeadPlayerGetSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken)
    {
      var playerSessionEntity = await _animalFiveHeadContext.PlayerSessionInformations
        .FromSqlRaw("EXECUTE dbo.pr_GetGameSession")
        .AsNoTracking()
        .ToListAsync(cancellationToken: cancellationToken);

      var recordSession = playerSessionEntity.Select(x => new AnimalFiveHeadPlayerGetSessionData
      {
        CardIds = x.CardIds,
        PlayerId = x.PlayerId,
        SessionId = x.SessionId
      });
      return recordSession;
    }

    public async Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken cancellationToken)
    {
      await _animalFiveHeadContext.PlayerSessionInformations
       .Where(x => x.SessionId == gameSessionId)
       .ForEachAsync(x => x.GameSession = "Complete", cancellationToken: cancellationToken);

      await _animalFiveHeadContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> GameSessionExistsAndActiveAsync(Guid gameSessionId, CancellationToken cancellationToken)
    {
      var haveSession = await _animalFiveHeadContext.PlayerSessionInformations
        .Where(x => x.SessionId == gameSessionId && x.GameSession == "Active")
        .AsNoTracking()
        .AnyAsync(cancellationToken: cancellationToken);

      return haveSession;

      //var sqlParams = new SqlParameter[] {
      //  new SqlParameter("SessionId",gameSessionId )
      //};

      //var playerSessionEntity = await _animalFiveHeadContext.PlayerSessionInformations.
      //  .FromSqlRaw("EXECUTE dbo.pr_GameSessionExistAndActive")
      //  .AsNoTracking()
      //  .To(cancellationToken: token);
    }
  }
}

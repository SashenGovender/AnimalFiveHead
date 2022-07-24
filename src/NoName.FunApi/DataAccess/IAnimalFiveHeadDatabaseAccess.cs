using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Domain.AnimalFiveHead;

namespace NoName.FunApi.DataAccess
{
  public interface IAnimalFiveHeadDatabaseAccess
  {
    Task<IEnumerable<AnimalFiveHeadPlayerGetSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken token);
    Task UpsertPlayerSessionInformationAsync(AnimalFiveHeadPlayerSaveSessionData newPlayerData, CancellationToken token);
    Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken token);
    Task<bool> GameSessionExistsAndActiveAsync(Guid gameSessionId, CancellationToken token);
  }
}

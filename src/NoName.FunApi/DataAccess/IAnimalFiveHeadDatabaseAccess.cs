using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Domain.AnimalFiveHead;

namespace NoName.FunApi.DataAccess
{
  public interface IAnimalFiveHeadDatabaseAccess
  {
    Task<IEnumerable<AnimalFiveHeadPlayerGetSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken cancellationToken);
    Task UpsertPlayerSessionInformationAsync(AnimalFiveHeadPlayerSaveSessionData newPlayerData, CancellationToken cancellationToken);
    Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken cancellationToken);
    Task<bool> GameSessionExistsAndActiveAsync(Guid gameSessionId, CancellationToken cancellationToken);
  }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Domain.AnimalFiveHead;

namespace NoName.FunApi.DataAccess
{
  public interface IAnimalFiveDatabaseAccess
  {
    Task<IEnumerable<AnimalFivePlayerGetSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken token);
    Task UpsertPlayerSessionInformationAsync(AnimalFivePlayerSaveSessionData newPlayerData, CancellationToken token);
    Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken token);
    Task<bool> GameSessionExistsAndActiveAsync(Guid gameSessionId, CancellationToken token);
  }
}

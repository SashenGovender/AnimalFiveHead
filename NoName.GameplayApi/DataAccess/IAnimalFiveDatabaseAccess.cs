using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NoName.GameplayApi.Models.AnimalFive.Dto;

namespace NoName.GameplayApi.DataAccess
{
  public interface IAnimalFiveDatabaseAccess
  {
    Task<IEnumerable<AnimalFivePlayerSessionData>> GetAllAsync(CancellationToken token);
    Task<IEnumerable<AnimalFivePlayerSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken token);
    Task UpsertPlayerSessionInformationAsync(AnimalFivePlayerSessionData newPlayerData, CancellationToken token);
    Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken token);
  }
}

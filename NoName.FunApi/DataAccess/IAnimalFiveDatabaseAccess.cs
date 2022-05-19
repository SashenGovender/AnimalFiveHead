using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NoName.FunApi.Models.AnimalFive.Dto;

namespace NoName.FunApi.DataAccess
{
  public interface IAnimalFiveDatabaseAccess
  {
    Task<IEnumerable<AnimalFivePlayerSessionData>> GetAllAsync(CancellationToken token);
    Task<IEnumerable<AnimalFivePlayerSessionData>> GetBySessionIdAsync(Guid sessionId, CancellationToken token);
    Task UpsertPlayerSessionInformationAsync(AnimalFivePlayerSessionData newPlayerData, CancellationToken token);
    Task CompleteGameSessionAsync(Guid gameSessionId, CancellationToken token);
  }
}

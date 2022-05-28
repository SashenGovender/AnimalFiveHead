using System;
using System.Threading;
using System.Threading.Tasks;
using NoName.FunApi.Models.AnimalFive;

namespace NoName.FunApi.GameManager
{
  public interface IAnimalFiveManager
  {
    public Task<AnimalFivePlayResponse> BeginPlayAsync(AnimalFivePlayRequest request, CancellationToken token);
    public Task<AnimalFiveChainResponse> ChainAsync(AnimalFiveChainRequest request, CancellationToken token);
    public Task<AnimalFiveCompleteGameResponse> CompleteGameAsync(AnimalFiveCompleteGameRequest request, CancellationToken token);
    public Task<bool> IsValidSessionGuid(Guid sessionId, CancellationToken token);
  }
}

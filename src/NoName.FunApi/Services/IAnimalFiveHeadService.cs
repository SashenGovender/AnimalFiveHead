using System;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Contract.AnimalFiveHead;

namespace NoName.FunApi.Services
{
  public interface IAnimalFiveHeadService
  {
    public Task<AnimalFiveHeadPlayResponse> BeginPlayAsync(AnimalFiveHeadPlayRequest request, CancellationToken token);
    public Task<AnimalFiveHeadChainResponse> ChainAsync(AnimalFiveHeadChainRequest request, CancellationToken token);
    public Task<AnimalFiveHeadCompleteGameResponse> CompleteGameAsync(AnimalFiveHeadCompleteGameRequest request, CancellationToken token);
    public Task<bool> IsValidSessionGuid(Guid sessionId, CancellationToken token);
  }
}

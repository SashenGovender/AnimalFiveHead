using System.Threading;
using System.Threading.Tasks;
using NoName.GameplayApi.Models.AnimalFive;

namespace NoName.GameplayApi.GameManager
{
  public interface IAnimalFiveManager
  {
    public Task<AnimalFivePlayResponse> BeginPlayAsync(AnimalFivePlayRequest request, CancellationToken token);
    public Task<AnimalFiveChainResponse> ChainAsync(AnimalFiveChainRequest request, CancellationToken token);
    public Task<AnimalFiveCompleteGameResponse> CompleteGameAsync(AnimalFiveCompleteGameRequest request, CancellationToken token);
  }
}

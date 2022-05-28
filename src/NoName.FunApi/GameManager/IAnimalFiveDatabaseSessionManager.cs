using System;
using System.Threading;
using System.Threading.Tasks;
using Game.AnimalFiveHead;
using Game.AnimalFiveHead.Player;

namespace NoName.FunApi.GameManager
{
  public interface IAnimalFiveDatabaseSessionManager
  {
    public Guid GameSessionId { get; }
    public void CreateOrSetSessionId(Guid? sessionId = null);
    public void SetGame(IAnimalFive animalFiveGame);
    public Task SaveGameStateAsync(CancellationToken token);
    public Task UpdatePlayerGameStateAsync(BasePlayer newPlayerData, CancellationToken token);
    public Task CompleteGameSessionAsync(CancellationToken token);
    public Task RestoreGameStateAsync(CancellationToken token);
    public Task<bool> CheckIfValidSessionId(Guid sessionId, CancellationToken token);
  }
}

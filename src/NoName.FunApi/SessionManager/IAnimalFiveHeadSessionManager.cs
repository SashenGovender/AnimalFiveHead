using System;
using System.Threading;
using System.Threading.Tasks;
using Game.AnimalFiveHead;
using Game.AnimalFiveHead.Player;

namespace NoName.FunApi.SessionManager
{
  public interface IAnimalFiveHeadSessionManager
  {
    public Guid GameSessionId { get; }
    public void CreateOrSetSessionId(Guid? sessionId = null);
    public void SetGame(IAnimalFiveHeadGame animalFiveGame);
    public Task SaveGameStateAsync(CancellationToken token);
    public Task UpdatePlayerGameStateAsync(BasePlayer newPlayerData, CancellationToken token);
    public Task CompleteGameSessionAsync(CancellationToken token);
    public Task RestoreGameStateAsync(CancellationToken token);
    public Task<bool> CheckIfValidSessionId(Guid sessionId, CancellationToken token);
  }
}

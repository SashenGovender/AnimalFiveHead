using System;
using System.Threading;
using System.Threading.Tasks;
using Common.PlayingCards.CardDecks;
using Common.PlayingCards.Enums;
using Game.AnimalFiveHead;
using NoName.FunApi.Models.AnimalFive;

namespace NoName.FunApi.GameManager
{
  public class AnimalFiveManager : IAnimalFiveManager
  {
    private readonly IAnimalFiveDatabaseSessionManager _gameSessionManager;
    private readonly IAnimalFive _animalFiveGame;

    public AnimalFiveManager(IAnimalFiveDatabaseSessionManager gameSessionManager, IAnimalFive animalFiveGame, DeckFactory deckFactory)
    {
      _gameSessionManager = gameSessionManager;
      _animalFiveGame = animalFiveGame;

      var deck = deckFactory.CreateDeck(DeckType.AnimalFiveHead);
      _animalFiveGame.InitialiseDeck(deck);
      _gameSessionManager.SetGame(_animalFiveGame);
    }

    public async Task<AnimalFivePlayResponse> BeginPlayAsync(AnimalFivePlayRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateOrSetSessionId();

      _animalFiveGame.AddRealPlayers(request.NumberOfPlayers);

      _animalFiveGame.BeginGame();

      await _gameSessionManager.SaveGameStateAsync(token);

      var playResponse = new AnimalFivePlayResponse(_animalFiveGame.Keeper, _animalFiveGame.Tourist, _animalFiveGame.Players, _gameSessionManager.GameSessionId);
      return playResponse;
    }

    public async Task<AnimalFiveChainResponse> ChainAsync(AnimalFiveChainRequest request, CancellationToken token)
    {
      var receivedSessionId = Guid.Parse(request.SessionId!);
      _gameSessionManager.CreateOrSetSessionId(receivedSessionId);

      await _gameSessionManager.RestoreGameStateAsync(token);

      var player = _animalFiveGame.Chain(request.PlayerId);

      await _gameSessionManager.UpdatePlayerGameStateAsync(player, token);

      var chainResponse = new AnimalFiveChainResponse(request, player);
      return chainResponse;
    }

    public async Task<AnimalFiveCompleteGameResponse> CompleteGameAsync(AnimalFiveCompleteGameRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateOrSetSessionId(Guid.Parse(request.SessionId!));

      await _gameSessionManager.RestoreGameStateAsync(token);

      _animalFiveGame.PlayNpcRound();

      _animalFiveGame.EndGame();

      await _gameSessionManager.CompleteGameSessionAsync(token);

      var gameResults = new AnimalFiveCompleteGameResponse(request.SessionId, _animalFiveGame);
      return gameResults;
    }

    public async Task<bool> IsValidSessionGuid(Guid sessionId, CancellationToken token) => await _gameSessionManager.CheckIfValidSessionId(sessionId, token);

  }
}


using System;
using System.Threading;
using System.Threading.Tasks;
using Common.PlayingCards;
using Common.PlayingCards.Enums;
using Game.AnimalFiveHead;
using NoName.FunApi.Models.AnimalFive;

namespace NoName.FunApi.GameManager
{
  public class AnimalFiveManager : IAnimalFiveManager
  {
    private readonly AnimalFiveGameSessionManager _gameSessionManager;
    private readonly IAnimalFive _animalFiveGame;
    private readonly DeckFactory _deckFactory;

    public AnimalFiveManager(AnimalFiveGameSessionManager gameSessionManager, IAnimalFive animalFiveGame, DeckFactory deckFactory)
    {
      _gameSessionManager = gameSessionManager;
      _animalFiveGame = animalFiveGame;
      _deckFactory = deckFactory;

      InitialiseGame();
    }

    public async Task<AnimalFivePlayResponse> BeginPlayAsync(AnimalFivePlayRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateSetSessionId();

      _animalFiveGame.AddRealPlayers(request.NumberOfPlayers);

      _animalFiveGame.BeginGame();

      await _gameSessionManager.SaveGameStateAsync(token);

      var playResponse = new AnimalFivePlayResponse(_animalFiveGame.Keeper, _animalFiveGame.Tourist, _animalFiveGame.Players, _gameSessionManager.GameSessionId);
      return playResponse;

      // JsonSerializer.Serialize(playResponse);
      // return JsonConvert.SerializeObject(playResponse);
    }

    public async Task<AnimalFiveChainResponse> ChainAsync(AnimalFiveChainRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateSetSessionId(Guid.Parse(request.SessionId!));

      await _gameSessionManager.RestoreGameStateAsync(token);

      var player = _animalFiveGame.Chain(request.PlayerId);

      await _gameSessionManager.UpdateGameStateAsync(player, token);

      var chainResponse = new AnimalFiveChainResponse(request, player);
      return chainResponse;
    }

    public async Task<AnimalFiveCompleteGameResponse> CompleteGameAsync(AnimalFiveCompleteGameRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateSetSessionId(Guid.Parse(request.SessionId!));

      await _gameSessionManager.RestoreGameStateAsync(token);

      _animalFiveGame.PlayNpcRound();

      _animalFiveGame.EndGame();

      await _gameSessionManager.CompleteGameSessionAsync(token);

      var gameResults = new AnimalFiveCompleteGameResponse(request.SessionId, _animalFiveGame);
      return gameResults;
    }

    private void InitialiseGame()
    {
      var deck = _deckFactory.CreateDeck(DeckType.AnimalFiveHead);

      _animalFiveGame.InitialiseDeck(deck);

      _gameSessionManager.SetGame(_animalFiveGame);
    }

  }
}


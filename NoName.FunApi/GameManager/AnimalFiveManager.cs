using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.PlayingCards;
using Common.PlayingCards.Enums;
using Game.AnimalFiveHead;
using Game.AnimalFiveHead.Player;
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

      for (var players = 1; players <= request.NumberOfPlayers; players++)
      {
        _animalFiveGame.Players.Add(new NormalPlayer(players));
      }

      _animalFiveGame.BeginPlay();

      await _gameSessionManager.SaveGameStateAsync(_animalFiveGame, token);

      var playResponse = new AnimalFivePlayResponse(_animalFiveGame.Keeper, _animalFiveGame.Tourist, _animalFiveGame.Players, _gameSessionManager.GameSessionId);
      return playResponse;

      // JsonSerializer.Serialize(playResponse);
      // return JsonConvert.SerializeObject(playResponse);
    }

    public async Task<AnimalFiveChainResponse> ChainAsync(AnimalFiveChainRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateSetSessionId(Guid.Parse(request.SessionId!));

      await _gameSessionManager.RestoreGameStateAsync(_animalFiveGame, token);

      var chainPlayActionResult = _animalFiveGame.Chain(request.PlayerId);

      await _gameSessionManager.UpdateGameStateAsync(chainPlayActionResult, token);

      var chainResponse = new AnimalFiveChainResponse(request, chainPlayActionResult);
      return chainResponse;
    }

    public async Task<AnimalFiveCompleteGameResponse> CompleteGameAsync(AnimalFiveCompleteGameRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateSetSessionId(Guid.Parse(request.SessionId!));

      await _gameSessionManager.RestoreGameStateAsync(_animalFiveGame, token);

      foreach (var player in _animalFiveGame.Players)
      {
        _animalFiveGame.CompletePlayerGame(player);
      }

      var gameResults = new AnimalFiveCompleteGameResponse(request.SessionId, _animalFiveGame.Keeper, _animalFiveGame.Tourist, _animalFiveGame.Players);
      return gameResults;
    }

    private void InitialiseGame()
    {
      var deck = _deckFactory.CreateDeck(DeckType.AnimalFiveHead);

      _animalFiveGame.Initialise(deck, new List<NormalPlayer>(), new KeeperPlayer(), new TouristPlayer());
    }

  }
}


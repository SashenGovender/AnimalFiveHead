using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Common.Models.Contract;
using Common.Models.Contract.AnimalFiveHead;
using Common.PlayingCards.CardDecks;
using Common.PlayingCards.Enums;
using Game.AnimalFiveHead;
using NoName.FunApi.SessionManager;

namespace NoName.FunApi.Services
{
  public class AnimalFiveHeadService : IAnimalFiveHeadService
  {
    private readonly IMapper _mapper;

    private readonly IAnimalFiveHeadSessionManager _gameSessionManager;
    private readonly IAnimalFiveHeadGame _game;

    public AnimalFiveHeadService(IAnimalFiveHeadSessionManager gameSessionManager, IAnimalFiveHeadGame animalFiveHeadGame, DeckFactory deckFactory, IMapper mapper)
    {
      _gameSessionManager = gameSessionManager;
      _game = animalFiveHeadGame;
      _mapper = mapper;

      var deck = deckFactory.CreateDeck(DeckType.AnimalFiveHead);
      _game.InitialiseDeck(deck);
      _gameSessionManager.SetGame(_game);

    }

    public async Task<AnimalFiveHeadPlayResponse> BeginPlayAsync(AnimalFiveHeadPlayRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateOrSetSessionId();

      _game.AddNpcPlayers();
      _game.AddRealPlayers(request.NumberOfPlayers);

      _game.BeginGame();

      await _gameSessionManager.SaveGameStateAsync(token);

      var npcPlayerResults = _mapper.Map<List<NpcPlayerResult>>(_game.NpcPlayers);
      var realPlayerResults = _mapper.Map<List<RealPlayerResult>>(_game.RealPlayers);
      var playResponse = new AnimalFiveHeadPlayResponse
      {
        NpcPlayerResults = npcPlayerResults,
        RealPlayerResults = realPlayerResults,
        SessionId = _gameSessionManager.GameSessionId.ToString()
      };
      return playResponse;
    }

    public async Task<AnimalFiveHeadChainResponse> ChainAsync(AnimalFiveHeadChainRequest request, CancellationToken token)
    {
      var receivedSessionId = Guid.Parse(request.SessionId!);
      _gameSessionManager.CreateOrSetSessionId(receivedSessionId);

      await _gameSessionManager.RestoreGameStateAsync(token);

      var player = _game.Chain(request.PlayerId);

      await _gameSessionManager.UpdatePlayerGameStateAsync(player, token);

      var playerCards = _mapper.Map<List<GameCard>>(player.Cards);
      var chainResponse = new AnimalFiveHeadChainResponse
      {
        PlayerId = request.PlayerId,
        Score = player.Score,
        SessionId = request.SessionId,
        PlayerCards = playerCards
      };
      return chainResponse;
    }

    public async Task<AnimalFiveHeadCompleteGameResponse> CompleteGameAsync(AnimalFiveHeadCompleteGameRequest request, CancellationToken token)
    {
      _gameSessionManager.CreateOrSetSessionId(Guid.Parse(request.SessionId!));

      await _gameSessionManager.RestoreGameStateAsync(token);

      _game.PlayNpcRound();

      _game.EndGame();

      await _gameSessionManager.CompleteGameSessionAsync(token);

      var npcPlayerResults = _mapper.Map<List<NpcPlayerResult>>(_game.NpcPlayers);
      var realPlayerResults = _mapper.Map<List<RealPlayerResult>>(_game.RealPlayers);
      var gameResults = new AnimalFiveHeadCompleteGameResponse
      {
        SessionId = request.SessionId,

        NpcPlayerResults = npcPlayerResults,
        RealPlayerResults = realPlayerResults
      };
      return gameResults;
    }

    public async Task<bool> IsValidSessionGuid(Guid sessionId, CancellationToken token) => await _gameSessionManager.CheckIfValidSessionId(sessionId, token);

  }
}


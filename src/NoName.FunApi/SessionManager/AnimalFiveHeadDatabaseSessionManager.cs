using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Domain.AnimalFiveHead;
using Game.AnimalFiveHead;
using Game.AnimalFiveHead.Enums;
using Game.AnimalFiveHead.Player;
using NoName.FunApi.DataAccess;

namespace NoName.FunApi.SessionManager
{
  public class AnimalFiveHeadDatabaseSessionManager : IAnimalFiveHeadSessionManager
  {
    private readonly IAnimalFiveDatabaseAccess _animalDatabaseAccess;
    private readonly IPlayerFactory _playerFactory;

    private IAnimalFiveHeadGame? _animalFiveHeadGame;

    public Guid GameSessionId { get; private set; }

    public AnimalFiveHeadDatabaseSessionManager(IAnimalFiveDatabaseAccess animalDatabaseAccess, IPlayerFactory playerFactory)
    {
      _animalDatabaseAccess = animalDatabaseAccess;
      _playerFactory = playerFactory;
    }

    public void CreateOrSetSessionId(Guid? sessionId = null) => GameSessionId = sessionId ?? Guid.NewGuid();

    public void SetGame(IAnimalFiveHeadGame animalFiveGame) => _animalFiveHeadGame = animalFiveGame;

    public async Task SaveGameStateAsync(CancellationToken token)
    {
      await SaveNpcGamePlayAsync(token);

      foreach (var player in _animalFiveHeadGame!.RealPlayers)
      {
        await UpdatePlayerGameStateAsync(player, token);
      }
    }

    public async Task UpdatePlayerGameStateAsync(BasePlayer newPlayerData, CancellationToken token)
    {
      var playerSessionDto = CreatePlayerSaveSession(newPlayerData);
      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(playerSessionDto, token);
    }

    public async Task CompleteGameSessionAsync(CancellationToken token)
    {
      await SaveGameStateAsync(token);

      await _animalDatabaseAccess.CompleteGameSessionAsync(GameSessionId, token);
    }

    public async Task RestoreGameStateAsync(CancellationToken token)
    {
      var gameData = await _animalDatabaseAccess.GetBySessionIdAsync(GameSessionId, token);

      foreach (var playerSessionData in gameData)
      {
        if (playerSessionData.PlayerId is > ((int)NpcPlayerType.NpcPlayerStartRange) and < ((int)NpcPlayerType.NpcPlayerEndRange))
        {
          var player = _playerFactory.GetNpcPlayer((NpcPlayerType)playerSessionData.PlayerId);
          RestorePlayerCards(player, playerSessionData);
          _animalFiveHeadGame!.RealPlayers.Add(player);
        }
        else
        {
          var player = _playerFactory.GetRealPlayer(playerSessionData.PlayerId);
          RestorePlayerCards(player, playerSessionData);
          _animalFiveHeadGame!.NpcPlayers.Add(player);
        }
      }
    }

    public async Task<bool> CheckIfValidSessionId(Guid sessionId, CancellationToken token) => await _animalDatabaseAccess.GameSessionExistsAndActiveAsync(sessionId, token);

    private void RestorePlayerCards(BasePlayer player, AnimalFivePlayerGetSessionData playerSessionData)
    {
      var cardIds = playerSessionData.CardIds!.Split(';');
      foreach (var cardId in cardIds)
      {
        var cardIntId = int.Parse(cardId, new CultureInfo("en-US"));
        var card = _animalFiveHeadGame!.CardDeck!.GetCard(cardIntId);
        player.AddCard(card!);
      }
    }

    private async Task SaveNpcGamePlayAsync(CancellationToken token)
    {
      //TODO: Design: Dapper to return multiple results
      foreach (var player in _animalFiveHeadGame!.NpcPlayers)
      {
        var playerDto = CreatePlayerSaveSession(player);
        await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(playerDto, token);
      }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0022:Use expression body for methods")]
    private AnimalFivePlayerSaveSessionData CreatePlayerSaveSession(BasePlayer player)
    {
      return new AnimalFivePlayerSaveSessionData()
      {
        Cards = string.Join(";", player.Cards),
        CardIds = string.Join(";", player.Cards.Select(card => card.CardId)),
        PlayerId = player.PlayerId,
        Score = player.Score,
        SessionId = GameSessionId,
        Result = player.GameStatus
      };
    }
  }
}

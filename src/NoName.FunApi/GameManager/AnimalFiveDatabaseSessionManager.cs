using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Game.AnimalFiveHead;
using Game.AnimalFiveHead.Player;
using NoName.FunApi.DataAccess;
using NoName.FunApi.Models.AnimalFive.Dto;

namespace NoName.FunApi.GameManager
{
  public class AnimalFiveDatabaseSessionManager : IAnimalFiveDatabaseSessionManager
  {
    private readonly IAnimalFiveDatabaseAccess _animalDatabaseAccess;
    private IAnimalFive? _animalFiveHeadGame;

    public Guid GameSessionId { get; private set; }

    public AnimalFiveDatabaseSessionManager(IAnimalFiveDatabaseAccess animalDatabaseAccess)
    {
      _animalDatabaseAccess = animalDatabaseAccess;
    }

    public void CreateOrSetSessionId(Guid? sessionId = null) => GameSessionId = sessionId ?? Guid.NewGuid();

    public void SetGame(IAnimalFive animalFiveGame) => _animalFiveHeadGame = animalFiveGame;

    public async Task SaveGameStateAsync(CancellationToken token)
    {
      await SaveNpcGamePlayAsync(token);

      foreach (var player in _animalFiveHeadGame!.Players)
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
        var player = PlayerToRestore(playerSessionData.PlayerId);
        RestorePlayerCards(player, playerSessionData);
      }
    }

    public async Task<bool> CheckIfValidSessionId(Guid sessionId, CancellationToken token) => await _animalDatabaseAccess.GameSessionExistsAndActiveAsync(sessionId, token);

    private BasePlayer PlayerToRestore(int playerId)
    {
      switch (playerId)
      {
        case AnimalFiveHeadConstants.TouristId:
          return _animalFiveHeadGame!.Tourist;

        case AnimalFiveHeadConstants.KeeperId:
          return _animalFiveHeadGame!.Keeper;

        default:
          var player = new RealPlayer(playerId);
          _animalFiveHeadGame!.Players.Add(player);
          return player;
      }
    }

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
      var touristDto = CreatePlayerSaveSession(_animalFiveHeadGame!.Tourist);
      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(touristDto, token);

      var keeperDto = CreatePlayerSaveSession(_animalFiveHeadGame.Keeper);
      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(keeperDto, token);
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

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
  public class AnimalFiveGameSessionManager
  {
    private readonly IAnimalFiveDatabaseAccess _animalDatabaseAccess;
    private IAnimalFive? _animalFiveHeadGame;

    public Guid GameSessionId { get; private set; }

    public AnimalFiveGameSessionManager(IAnimalFiveDatabaseAccess animalDatabaseAccess)
    {
      _animalDatabaseAccess = animalDatabaseAccess;
    }

    public void CreateSetSessionId(Guid? sessionId = null) => GameSessionId = sessionId ?? Guid.NewGuid();

    public void SetGame(IAnimalFive animalFiveGame) => _animalFiveHeadGame = animalFiveGame;

    public async Task CompleteGameSessionAsync(CancellationToken token) => await _animalDatabaseAccess.CompleteGameSessionAsync(GameSessionId, token);

    public async Task SaveGameStateAsync(CancellationToken token)
    {
      var touristDto = CreatePlayerSessionObject(_animalFiveHeadGame!.Tourist, GameSessionId);
      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(touristDto, token);

      var keeperDto = CreatePlayerSessionObject(_animalFiveHeadGame.Keeper, GameSessionId);
      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(keeperDto, token);

      foreach (var player in _animalFiveHeadGame.Players)
      {
        var playerSessionDto = CreatePlayerSessionObject(player, GameSessionId);
        await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(playerSessionDto, token);
      }
    }

    public async Task UpdateGameStateAsync(BasePlayer newPlayerData, CancellationToken token)
    {
      var playerSessionDto = CreatePlayerSessionObject(newPlayerData, GameSessionId);

      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(playerSessionDto, token);
    }

    public async Task RestoreGameStateAsync(CancellationToken token)
    {
      var gameData = await _animalDatabaseAccess.GetBySessionIdAsync(GameSessionId, token);

      foreach (var playerSessionData in gameData)
      {
        if (playerSessionData.PlayerId == AnimalFiveHeadConstants.TouristId)
        {
          RestorePlayerCards(_animalFiveHeadGame!.Tourist, playerSessionData);
        }
        else if (playerSessionData.PlayerId == AnimalFiveHeadConstants.KeeperId)
        {
          RestorePlayerCards(_animalFiveHeadGame!.Keeper, playerSessionData);
        }
        else
        {
          var player = new NormalPlayer(playerSessionData.PlayerId);
          _animalFiveHeadGame!.Players.Add(player);
          RestorePlayerCards(player, playerSessionData);
        }
      }
    }

    private void RestorePlayerCards(BasePlayer player, AnimalFivePlayerSessionData playerSessionData)
    {
      var cardIds = playerSessionData.CardIds!.Split(';');
      foreach (var cardId in cardIds)
      {
        var cardIntId = int.Parse(cardId, new CultureInfo("en-US"));
        var card = _animalFiveHeadGame!.CardDeck!.GetCard(cardIntId);
        player.AddCard(card!);
      }
    }

    private static AnimalFivePlayerSessionData CreatePlayerSessionObject(BasePlayer player, Guid sessionGuid)
    {
#pragma warning disable IDE0022 // Use expression body for methods
      return new AnimalFivePlayerSessionData()
      {
        Cards = string.Join(";", player.Cards),
        CardIds = string.Join(";", player.Cards.Select(card => card.CardId)),
        PlayerId = player.PlayerId,
        Score = player.Score,
        SessionId = sessionGuid.ToString(),
      };
    }

  }
}

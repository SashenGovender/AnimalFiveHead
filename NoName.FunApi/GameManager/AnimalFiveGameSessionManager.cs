using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead;
using Game.AnimalFiveHead.Player;
using NoName.FunApi.DataAccess;
using NoName.FunApi.Models.AnimalFive.Dto;

namespace NoName.FunApi.GameManager
{
  public class AnimalFiveGameSessionManager
  {
    private readonly IAnimalFiveDatabaseAccess _animalDatabaseAccess;

    public Guid GameSessionId { get; private set; }

    public AnimalFiveGameSessionManager(IAnimalFiveDatabaseAccess animalDatabaseAccess)
    {
      _animalDatabaseAccess = animalDatabaseAccess;
    }

    public void CreateSetSessionId(Guid? sessionId = null)
    {
#pragma warning disable IDE0022 // Use expression body for methods
      GameSessionId = sessionId ?? Guid.NewGuid();
#pragma warning restore IDE0022 // Use expression body for methods
    }

    public async Task SaveGameStateAsync(IAnimalFive animalFiveHead, CancellationToken token)
    {
      var touristDto = CreatePlayerSessionObject(animalFiveHead.Tourist, GameSessionId);
      var keeperDto = CreatePlayerSessionObject(animalFiveHead.Keeper, GameSessionId);

      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(touristDto, token);
      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(keeperDto, token);

      foreach (var player in animalFiveHead.Players)
      {
        var playerSessionDto = CreatePlayerSessionObject(player, GameSessionId);
        await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(playerSessionDto, token);
      }
    }

    public async Task UpdateGameStateAsync(IPlayer newPlayerData, CancellationToken token)
    {
      var playerSessionDto = CreatePlayerSessionObject(newPlayerData, GameSessionId);

      await _animalDatabaseAccess.UpsertPlayerSessionInformationAsync(playerSessionDto, token);
    }

    public async Task CompleteGameSessionAsync(CancellationToken token)
    {
#pragma warning disable IDE0022 // Use expression body for methods
      await _animalDatabaseAccess.CompleteGameSessionAsync(GameSessionId, token);
#pragma warning restore IDE0022 // Use expression body for methods
    }

    public async Task RestoreGameStateAsync(IAnimalFive animalFive, CancellationToken token)
    {
      var gameData = await _animalDatabaseAccess.GetBySessionIdAsync(GameSessionId, token);

      foreach (var playerSessionData in gameData)
      {
        NormalPlayer player = null!;
        if (playerSessionData.PlayerId == AnimalFiveHeadConstants.TouristId)
        {
          player = animalFive.Tourist;
        }
        else if (playerSessionData.PlayerId == AnimalFiveHeadConstants.KeeperId)
        {
          player = animalFive.Keeper;
        }
        else
        {
          player = new NormalPlayer(playerSessionData.PlayerId);
          animalFive.Players.Add(player);
        }

        var cardIds = playerSessionData.PlayerCardIds.Split(';');
        foreach (var cardId in cardIds)
        {
          var cardIntId = int.Parse(cardId, new CultureInfo("en-US"));
          player.AddCard(new PlayCard(cardIntId));

          animalFive.CardDeck.GetCard(cardIntId);
        }
      }
    }

    private static AnimalFivePlayerSessionData CreatePlayerSessionObject(IPlayer player, Guid sessionGuid)
    {
#pragma warning disable IDE0022 // Use expression body for methods
      return new AnimalFivePlayerSessionData()
      {
        PlayerCards = string.Join(";", player.Cards),
        PlayerCardIds = string.Join(";", player.Cards.Select(card => card.CardId)),
        PlayerId = player.PlayerId,
        Score = player.Score,
        SessionId = sessionGuid.ToString(),
      };
    }

  }
}

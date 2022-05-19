using System.Collections.Generic;
using System.Linq;
using Common.PlayingCards.CardDecks;
using Common.PlayingCards.Exceptions;
using Common.PlayingCards.Models;
using Game.AnimalFive.Enums;
using Game.AnimalFive.Player;

namespace Game.AnimalFive
{
  public class AnimalFiveHead : IAnimalFive
  {
    public TouristPlayer Tourist { get; private set; } = null!;
    public KeeperPlayer Keeper { get; private set; } = null!;
    public IDeck CardDeck { get; private set; } = null!;
    public List<NormalPlayer> Players { get; private set; } = null!;

    public AnimalFiveHead()
    {
    }

    public void Initialise(IDeck cardDeck, List<NormalPlayer> players, KeeperPlayer keeper, TouristPlayer tourist)
    {
      CardDeck = cardDeck;
      Players = players;
      Keeper = keeper;
      Tourist = tourist;
    }

    public void BeginPlay()
    {
      foreach (var player in Players)
      {
        player.AddCard(GetCard());
      }

      Keeper.AddCard(GetCard());
      Tourist.AddCard(GetCard());
    }

    public IPlayer Chain(int playerId)
    {
      if (playerId is AnimalFiveHeadConstants.KeeperId)
      {
        while (Keeper.Cards.Count <= 4)
        {
          Keeper.Cards.Add(GetCard());
        }
        return Keeper;
      }
      else if (playerId is AnimalFiveHeadConstants.TouristId)
      {
        while (Tourist.Cards.Count <= 4)
        {
          Tourist.Cards.Add(GetCard());
        }
        return Tourist;
      }
      else
      {
        var player = Players.First(player => player.PlayerId == playerId);
        player.AddCard(GetCard());
        return player;
      }
    }

    public void CompletePlayerGame(IPlayer player)
    {
      var playerScore = player.Score;
      var keeperScore = Keeper.Score;
      var touristScore = Tourist.Score;

      if (playerScore > keeperScore && playerScore > touristScore)
      {
        player.GameStatus = GameStatus.PlayerWin;
      }
      else if (playerScore > keeperScore && playerScore < touristScore)
      {
        player.GameStatus = GameStatus.TouristWin;
      }
      else if (playerScore < keeperScore && playerScore > touristScore)
      {
        player.GameStatus = GameStatus.KeeperWin;
      }
      else
      {
        player.GameStatus = GameStatus.Draw;
      }
    }

    private Card GetCard() => CardDeck.GetCard() ?? throw new NoCardException();
  }
}

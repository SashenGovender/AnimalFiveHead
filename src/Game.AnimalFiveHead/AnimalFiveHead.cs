using System.Collections.Generic;
using System.Linq;
using Common.PlayingCards.CardDecks;
using Common.PlayingCards.Exceptions;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;
using Game.AnimalFiveHead.Player;

namespace Game.AnimalFiveHead
{
  public class AnimalFiveHead : IAnimalFive
  {
    private readonly IPlayerFactory _playerFactory;

    public BasePlayer Tourist { get; init; }
    public BasePlayer Keeper { get; init; }
    public List<BasePlayer> Players { get; init; }
    public IDeck? CardDeck { get; private set; }

    public AnimalFiveHead(IPlayerFactory playerFactory)
    {
      _playerFactory = playerFactory;

      Players = new List<BasePlayer>();
      Keeper = _playerFactory.GetPlayer(AnimalFiveHeadConstants.KeeperId);
      Tourist = _playerFactory.GetPlayer(AnimalFiveHeadConstants.TouristId);
    }

    public void InitialiseDeck(IDeck cardDeck) => CardDeck = cardDeck;

    public void AddRealPlayers(int numberOfPlayers)
    {
      for (var playerId = 0; playerId < numberOfPlayers; playerId++)
      {
        Players.Add(_playerFactory.GetPlayer(playerId));
      }
    }

    public void BeginGame()
    {
      foreach (var player in Players)
      {
        player.AddCard(GetCard());
      }

      Keeper.AddCard(GetCard());
      Tourist.AddCard(GetCard());
    }

    public BasePlayer Chain(int playerId)
    {
      var player = Players.First(player => player.PlayerId == playerId);
      player.Chain(GetCard);

      return player;
    }

    public void PlayNpcRound()
    {
      Tourist.Chain(GetCard);
      Keeper.Chain(GetCard);
    }

    public void EndGame()
    {
      var keeperScore = Keeper.Score;
      var touristScore = Tourist.Score;

      foreach (var player in Players)
      {
        var playerScore = player.Score;
        if (playerScore > keeperScore && playerScore > touristScore)
        {
          player.GameStatus = PlayerMatchResult.PlayerWin;
        }
        else if (playerScore < keeperScore || playerScore < touristScore)
        {
          player.GameStatus = PlayerMatchResult.NpcWin;
        }
        else
        {
          player.GameStatus = PlayerMatchResult.Draw;
        }
      }
    }

    private PlayCard GetCard() => CardDeck!.GetCard() ?? throw new NoCardException();
  }
}

using System.Collections.Generic;
using System.Linq;
using Common.PlayingCards.CardDecks;
using Common.PlayingCards.Exceptions;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;
using Game.AnimalFiveHead.Player;

namespace Game.AnimalFiveHead
{
  public class AnimalFiveHeadGame : IAnimalFiveHeadGame
  {
    private readonly IPlayerFactory _playerFactory;

    public List<BasePlayer> NpcPlayers { get; init; }
    public List<BasePlayer> RealPlayers { get; init; }
    public IDeck? CardDeck { get; private set; }

    public AnimalFiveHeadGame(IPlayerFactory playerFactory)
    {
      _playerFactory = playerFactory;

      RealPlayers = new List<BasePlayer>();
      NpcPlayers = new List<BasePlayer>();
    }

    public void InitialiseDeck(IDeck cardDeck) => CardDeck = cardDeck;

    public void AddRealPlayers(int numberOfPlayers)
    {
      for (var playerId = 0; playerId < numberOfPlayers; playerId++)
      {
        RealPlayers.Add(_playerFactory.GetRealPlayer(playerId));
      }
    }

    public void AddNpcPlayers()
    {
      NpcPlayers.Add(_playerFactory.GetNpcPlayer(NpcPlayerType.KeeperId));
      NpcPlayers.Add(_playerFactory.GetNpcPlayer(NpcPlayerType.TouristId));
    }

    public void BeginGame()
    {
      foreach (var realPlayer in RealPlayers)
      {
        realPlayer.AddCard(GetCard());
      }

      foreach (var npcPlayer in NpcPlayers)
      {
        npcPlayer.AddCard(GetCard());
      }
    }

    public BasePlayer Chain(int playerId)
    {
      var player = RealPlayers.First(player => player.PlayerId == playerId);
      player.Chain(GetCard);

      return player;
    }

    public void PlayNpcRound()
    {
      foreach (var npcPlayer in NpcPlayers)
      {
        npcPlayer.Chain(GetCard);
      }
    }

    public void EndGame()
    {
      var maxNpcPlayerScore = NpcPlayers.Max(player => player.Score);

      foreach (var player in RealPlayers)
      {
        var playerScore = player.Score;
        if (playerScore > maxNpcPlayerScore)
        {
          player.GameStatus = PlayerMatchResult.PlayerWin;
        }
        else if (playerScore < maxNpcPlayerScore)
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

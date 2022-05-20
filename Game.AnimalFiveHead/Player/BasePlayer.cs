using System;
using System.Collections.Generic;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Player
{
  public abstract class BasePlayer
  {
    public int Score => CalculateScore();
    public int PlayerId { get; init; }
    public List<PlayCard> Cards { get; init; }
    public PlayerMatchResult GameStatus { get; set; }
    public PlayCardFace ProtectedCardType { get; init; }

    protected BasePlayer(int playerId, PlayCardFace cardTypeToSave)
    {
      PlayerId = playerId;
      ProtectedCardType = cardTypeToSave;
      Cards = new List<PlayCard>();
    }

    public void AddCard(PlayCard card)
    {
      if (card != null)
      {
        Cards.Add(card);
      }
    }
    public abstract void Chain(Func<PlayCard> getCard);

    private int CalculateScore()
    {
      if (Cards.Count == 0)
      {
        return 0;
      }

      var sum = 0;
      var previousCard = Cards[0];

      for (var index = 1; index < Cards.Count; index++)
      {
        var rankDifference = Math.Abs(previousCard.Rank - Cards[index].Rank);
        if (rankDifference == 1 && previousCard.Face != ProtectedCardType)
        {
          sum = sum - previousCard.Value + Cards[index].Rank;
        }
        else
        {
          sum += Cards[index].Value;
        }
      }
      return sum;
    }
  }
}

using System;
using System.Collections.Generic;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;

namespace Game.AnimalFiveHead.Player
{
  public class NormalPlayer : IPlayer
  {
    private readonly PlayCardFace _protectedCardType;

    public int PlayerId { get; }
    public List<PlayCard> Cards { get; init; }
    public int Score => CalculateScore();
    public PlayerGameResult GameStatus { get; set; }

    public NormalPlayer(int playerId, PlayCardFace cardTypeToSave = PlayCardFace.None)
    {
      Cards = new List<PlayCard>();
      PlayerId = playerId;
      _protectedCardType = cardTypeToSave;
    }

    public void AddCard(PlayCard card)
    {
      if (card != null)
      {
        Cards.Add(card);
      }
    }

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
        if (rankDifference == 1 && previousCard.Face != _protectedCardType)
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

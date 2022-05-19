using System;
using System.Collections.Generic;
using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Game.AnimalFive.Enums;

namespace Game.AnimalFive.Player
{
  public class NormalPlayer : IPlayer
  {
    private readonly CardFace _protectedCardType;

    public int PlayerId { get; }
    public List<Card> Cards { get; init; }
    public int Score => CalculateScore();
    public GameStatus GameStatus { get; set; }

    public NormalPlayer(int playerId, CardFace cardTypeToSave = CardFace.None)
    {
      Cards = new List<Card>();
      PlayerId = playerId;
      _protectedCardType = cardTypeToSave;
    }

    public void AddCard(Card card)
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

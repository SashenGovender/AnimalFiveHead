using System.Collections.Generic;
using Common.PlayingCards.Models;
using Common.Utilities.Randomizer;

namespace Common.PlayingCards.CardDecks
{
  public abstract class BaseDeck : IDeck
  {
    private readonly IRandomNumberGenerator _randomNumberGenerator;
    private const int NumberTimesToShuffle = 5;

    public List<PlayCard> Cards { get; init; }

    protected BaseDeck(IRandomNumberGenerator randomNumberGenerator)
    {
      _randomNumberGenerator = randomNumberGenerator;
      Cards = new List<PlayCard>();
    }

    public void AddCard(PlayCard card)
    {
      if (card != null)
      {
        Cards.Add(card);
      }
    }

    public PlayCard? GetCard()
    {
      var numCards = Cards.Count;
      if (numCards <= 0)
      {
        return null;
      }

      var card = Cards[numCards - 1];
      Cards.RemoveAt(numCards - 1);

      return card;
    }

    public PlayCard? GetCard(int id)
    {
      var numCards = Cards.Count;
      if (numCards <= 0)
      {
        return null;
      }

      var card = Cards.Find(c => c.CardId == id);
      if (card is null)
      {
        return null;
      }

      Cards.Remove(card);

      return card;
    }

    //Design: Should this be part of the BaseDeck or a user of this object
    public void Shuffle()
    {
      var numCards = Cards.Count;
      for (var times = 0; times < NumberTimesToShuffle; times++)
      {
        var cardIndex1 = _randomNumberGenerator.GetNumber(0, numCards);
        var cardIndex2 = _randomNumberGenerator.GetNumber(0, numCards);

        Swap(cardIndex1, cardIndex2);
      }
    }

    private void Swap(int firstIndex, int secondIndex)
    {
      var tmp = Cards[firstIndex];
      Cards[firstIndex] = Cards[secondIndex];
      Cards[secondIndex] = tmp;
    }

  }
}

using System.Collections.Generic;
using System.Linq;
using Common.PlayingCards.Models;
using Common.Utilities.Randomizer;

namespace Common.PlayingCards.CardDecks
{
  public abstract class BaseDeck : IDeck
  {
    private readonly IRandomNumberGenerator _randomNumberGenerator;
    private const int NumberTimesToShuffle = 5;

    public List<PlayCard> Cards { get; }

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

      var deckHasCard = Cards.Exists(c => c.CardId == id);
      if (!deckHasCard)
      {
        return null;
      }

      var card = Cards.FirstOrDefault(c => c.CardId == id);
      Cards.Remove(card!);

      return card;
    }

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

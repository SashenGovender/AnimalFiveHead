using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Common.Utilities.Randomizer;

namespace Common.PlayingCards.CardDecks
{
  public class StandardDeck : BaseDeck
  {
    public StandardDeck(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator)
    {
      for (var cardFace = (int)CardFace.Ace; cardFace <= (int)CardFace.King; cardFace++)
      {
        var value = cardFace > 10 ? 10 : cardFace;
        AddCard(new Card((CardFace)cardFace, CardSuit.Clubs, value));
        AddCard(new Card((CardFace)cardFace, CardSuit.Diamonds, value));
        AddCard(new Card((CardFace)cardFace, CardSuit.Hearts, value));
        AddCard(new Card((CardFace)cardFace, CardSuit.Spades, value));
      }

      Shuffle();
    }
  }
}

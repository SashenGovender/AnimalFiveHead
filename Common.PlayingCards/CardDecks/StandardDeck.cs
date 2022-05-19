using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Common.Utilities.Randomizer;

namespace Common.PlayingCards.CardDecks
{
  public class StandardDeck : BaseDeck
  {
    public StandardDeck(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator)
    {
      for (var cardFace = (int)PlayCardFace.Ace; cardFace <= (int)PlayCardFace.King; cardFace++)
      {
        AddCard(new PlayCard((PlayCardFace)cardFace, PlayCardType.Clubs, cardFace));
        AddCard(new PlayCard((PlayCardFace)cardFace, PlayCardType.Diamonds, cardFace));
        AddCard(new PlayCard((PlayCardFace)cardFace, PlayCardType.Hearts, cardFace));
        AddCard(new PlayCard((PlayCardFace)cardFace, PlayCardType.Spades, cardFace));
      }

      Shuffle();
    }
  }
}

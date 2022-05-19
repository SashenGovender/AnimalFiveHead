using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Common.Utilities.Randomizer;

namespace Common.PlayingCards.CardDecks
{
  public class AnimalFiveHeadDeck : BaseDeck
  {
    public AnimalFiveHeadDeck(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator)
    {
      for (var cardSuit = (int)PlayCardType.Bronze; cardSuit <= (int)PlayCardType.Gold; cardSuit++)
      {
        AddCard(new PlayCard(PlayCardFace.Grass, (PlayCardType)cardSuit, 5, 1));
        AddCard(new PlayCard(PlayCardFace.Flower, (PlayCardType)cardSuit, 5, 1));
        AddCard(new PlayCard(PlayCardFace.Leave, (PlayCardType)cardSuit, 5, 1));

        AddCard(new PlayCard(PlayCardFace.Catipilar, (PlayCardType)cardSuit, 4, 2));
        AddCard(new PlayCard(PlayCardFace.HoneyBee, (PlayCardType)cardSuit, 4, 2));

        AddCard(new PlayCard(PlayCardFace.SparrowBird, (PlayCardType)cardSuit, 5, 3));
        AddCard(new PlayCard(PlayCardFace.Mouse, (PlayCardType)cardSuit, 5, 3));
        AddCard(new PlayCard(PlayCardFace.Rabbit, (PlayCardType)cardSuit, 4, 3));

        AddCard(new PlayCard(PlayCardFace.Snake, (PlayCardType)cardSuit, 7, 4));
        AddCard(new PlayCard(PlayCardFace.Fox, (PlayCardType)cardSuit, 7, 4));

        AddCard(new PlayCard(PlayCardFace.Lion, (PlayCardType)cardSuit, 8, 5));
      }
      AddCard(new PlayCard(PlayCardFace.Fox, PlayCardType.Diamond, 10, 4));
      AddCard(new PlayCard(PlayCardFace.Lion, PlayCardType.Diamond, 10, 5));

      Shuffle();
    }
  }
}

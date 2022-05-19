using Common.PlayingCards.Enums;
using Common.PlayingCards.Models;
using Common.Utilities.Randomizer;

namespace Common.PlayingCards.CardDecks
{
  public class AnimalFiveHeadDeck : BaseDeck
  {
    public AnimalFiveHeadDeck(IRandomNumberGenerator randomNumberGenerator) : base(randomNumberGenerator)
    {
      for (var cardSuit = (int)CardSuit.Bronze; cardSuit <= (int)CardSuit.Gold; cardSuit++)
      {
        AddCard(new Card(CardFace.Grass, (CardSuit)cardSuit, 5, 1));
        AddCard(new Card(CardFace.Flower, (CardSuit)cardSuit, 5, 1));
        AddCard(new Card(CardFace.Leave, (CardSuit)cardSuit, 5, 1));

        AddCard(new Card(CardFace.Catipilar, (CardSuit)cardSuit, 4, 2));
        AddCard(new Card(CardFace.HoneyBee, (CardSuit)cardSuit, 4, 2));

        AddCard(new Card(CardFace.SparrowBird, (CardSuit)cardSuit, 5, 3));
        AddCard(new Card(CardFace.Mouse, (CardSuit)cardSuit, 5, 3));
        AddCard(new Card(CardFace.Rabbit, (CardSuit)cardSuit, 4, 3));

        AddCard(new Card(CardFace.Snake, (CardSuit)cardSuit, 7, 4));
        AddCard(new Card(CardFace.Fox, (CardSuit)cardSuit, 7, 4));

        AddCard(new Card(CardFace.Lion, (CardSuit)cardSuit, 8, 5));
      }
      AddCard(new Card(CardFace.Fox, CardSuit.Diamond, 10));
      AddCard(new Card(CardFace.Lion, CardSuit.Diamond, 10));

      Shuffle();
    }
  }
}

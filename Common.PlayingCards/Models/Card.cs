using Common.PlayingCards.Enums;

namespace Common.PlayingCards.Models
{
  public class Card
  {
    public CardFace Face { get; init; }
    public CardSuit Suit { get; init; }
    public int Value { get; init; }
    public int CardId { get; init; }
    public int Rank { get; init; }

    public Card(CardFace face, CardSuit suit, int value, int rank = 1)
    {
      Face = face;
      Suit = suit;
      Value = value;
      CardId = ((int)suit * 100) + (int)face;
      Rank = rank;
    }

    public Card(int cardId)
    {
      Face = (CardFace)(cardId % 100);
      Suit = (CardSuit)(cardId / 100);
      Value = cardId % 100;
      CardId = cardId;
    }

    public override string ToString() => $"{Face}{Suit}";
  }
}

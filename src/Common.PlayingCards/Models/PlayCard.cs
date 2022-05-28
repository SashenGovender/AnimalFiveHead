using Common.PlayingCards.Enums;

namespace Common.PlayingCards.Models
{
  public class PlayCard
  {
    public PlayCardFace Face { get; init; }
    public PlayCardType Type { get; init; }
    public int Value { get; init; }
    public int CardId { get; init; }
    public int Rank { get; init; }

    public PlayCard(PlayCardFace face, PlayCardType suit, int value, int rank = 1)
    {
      Face = face;
      Type = suit;
      Value = value;
      CardId = ((int)suit * 100) + (int)face;
      Rank = rank;
    }

    public PlayCard(int cardId)
    {
      Face = (PlayCardFace)(cardId % 100);
      Type = (PlayCardType)(cardId / 100);
      Value = cardId % 100;
      CardId = cardId;
    }

    public override string ToString() => $"{Face}{Type}";
  }
}

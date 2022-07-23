using Common.PlayingCards.Enums;

namespace Common.Models.Contract
{
  public class GameCard
  {
    public PlayCardFace Face { get; init; }
    public PlayCardType Type { get; init; }
    public int Value { get; init; }
    public int CardId { get; init; }
    public int Rank { get; init; }

  }
}

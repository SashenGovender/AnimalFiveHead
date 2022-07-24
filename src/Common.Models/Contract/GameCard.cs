using System.Text.Json.Serialization;
using Common.PlayingCards.Enums;

namespace Common.Models.Contract
{
  public class GameCard
  {
    [JsonPropertyName("playCardFace")]
    public PlayCardFace Face { get; init; }

    [JsonPropertyName("playCardType")]
    public PlayCardType Type { get; init; }

    [JsonPropertyName("rank")]
    public int Rank { get; init; }

  }
}

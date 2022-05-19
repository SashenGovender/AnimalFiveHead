using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.PlayingCards.Models;

namespace NoName.GameplayApi.Models.AnimalFive
{
  public class PlayerHand
  {
    [JsonPropertyName("playerId")]
    public int PlayerId { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("playersCards")]
    public List<Card>? PlayerCards { get; init; }
  }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.PlayingCards.Models;

namespace NoName.GameplayApi.Models.AnimalFive
{
  public class NpcHand
  {
    [JsonPropertyName("cards")]
    public List<Card>? Cards { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }
  }
}

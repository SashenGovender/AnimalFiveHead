using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.PlayingCards.Models;

namespace NoName.FunApi.Models.AnimalFive
{
  public class NpcBag
  {
    [JsonPropertyName("cards")]
    public List<PlayCard>? Cards { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }
  }
}

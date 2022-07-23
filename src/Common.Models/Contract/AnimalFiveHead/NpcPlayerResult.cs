using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class NpcPlayerResult
  {
    [JsonPropertyName("cards")]
    public List<GameCard>? Cards { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }
  }
}

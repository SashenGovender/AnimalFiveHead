using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class AnimalFiveHeadChainResponse
  {
    [JsonPropertyName("playerId")]
    public int PlayerId { get; init; }

    [JsonPropertyName("playerCards")]
    public List<GameCard>? PlayerCards { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }
  }
}

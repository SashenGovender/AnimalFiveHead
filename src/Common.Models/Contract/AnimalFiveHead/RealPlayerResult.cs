using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFiveHead.Enums;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class RealPlayerResult
  {
    [JsonPropertyName("playerId")]
    public int PlayerId { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("playersCards")]
    public List<GameCard>? PlayerCards { get; init; }

    [JsonPropertyName("result")]
    public PlayerMatchResult Result { get; init; }
  }
}

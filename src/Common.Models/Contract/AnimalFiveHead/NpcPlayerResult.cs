using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFiveHead.Enums;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class NpcPlayerResult
  {
    [JsonPropertyName("playerType")]
    public NpcPlayerType PlayerType { get; init; }

    [JsonPropertyName("cards")]
    public List<GameCard>? Cards { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }
  }
}

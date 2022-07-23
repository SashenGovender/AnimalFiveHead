using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class AnimalFiveHeadPlayResponse
  {
    [JsonPropertyName("npcPlayerResults")]
    public List<NpcPlayerResult>? NpcPlayerResults { get; init; }

    [JsonPropertyName("realPlayerResults")]
    public List<RealPlayerResult>? RealPlayerResults { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }
  }
}

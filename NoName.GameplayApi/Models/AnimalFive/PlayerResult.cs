using System.Text.Json.Serialization;
using Game.AnimalFive.Enums;

namespace NoName.GameplayApi.Models.AnimalFive
{
  public class PlayerResult
  {
    [JsonPropertyName("playerId")]
    public int PlayerId { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("result")]
    public GameStatus Result { get; init; }
  }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.PlayingCards.Models;
using Game.AnimalFiveHead.Enums;

namespace NoName.FunApi.Models.AnimalFive
{
  public class PlayerBag
  {
    [JsonPropertyName("playerId")]
    public int PlayerId { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("playersCards")]
    public List<PlayCard>? PlayerCards { get; init; }

    [JsonPropertyName("result")]
    public PlayerMatchResult Result { get; init; }
  }
}

using System.Collections.Generic;
using System.Text.Json.Serialization;
using Common.PlayingCards.Models;
using Game.AnimalFive.Player;

namespace NoName.GameplayApi.Models.AnimalFive
{
  public class AnimalFiveChainResponse
  {
    [JsonPropertyName("playerId")]
    public int PlayerId { get; init; }

    [JsonPropertyName("playerCards")]
    public List<Card>? PlayerCards { get; init; }

    [JsonPropertyName("score")]
    public int Score { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    public AnimalFiveChainResponse(AnimalFiveChainRequest request, IPlayer player)
    {
      PlayerId = request.PlayerId;
      PlayerCards = player.Cards;
      Score = player.Score;
      SessionId = request.SessionId;
    }
  }
}

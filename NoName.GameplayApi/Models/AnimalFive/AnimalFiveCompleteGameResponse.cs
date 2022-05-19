using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFive.Player;

namespace NoName.GameplayApi.Models.AnimalFive
{
  public class AnimalFiveCompleteGameResponse
  {
    [JsonPropertyName("tourist")]
    public NpcHand? Tourist { get; init; }

    [JsonPropertyName("keeper")]
    public NpcHand? Keeper { get; init; }

    [JsonPropertyName("players")]
    public List<PlayerResult>? Players { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    public AnimalFiveCompleteGameResponse(string? sessionId, KeeperPlayer keeper, TouristPlayer tourist, List<NormalPlayer> players)
    {
      SessionId = sessionId;

      Keeper = new NpcHand
      {
        Score = keeper.Score,
        Cards = keeper.Cards,
      };

      Tourist = new NpcHand
      {
        Score = tourist.Score,
        Cards = tourist.Cards,
      };

      Players = new List<PlayerResult>();
      foreach (var player in players)
      {
        Players.Add(new PlayerResult
        {
          PlayerId = player.PlayerId,
          Score = player.Score,
          Result = player.GameStatus
        });
      }

    }
  }
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFive.Player;

namespace NoName.GameplayApi.Models.AnimalFive
{
  public class AnimalFivePlayResponse
  {
    [JsonPropertyName("keeper")]
    public NpcHand? Keeper { get; init; }

    [JsonPropertyName("tourist")]
    public NpcHand? Tourist { get; init; }

    [JsonPropertyName("players")]
    public List<PlayerHand>? Players { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    public AnimalFivePlayResponse(KeeperPlayer keeper, TouristPlayer tourist, List<NormalPlayer> players, Guid gameSessionId)
    {
      Keeper = new NpcHand
      {
        Cards = keeper.Cards,
        Score = keeper.Score,
      };

      Tourist = new NpcHand
      {
        Cards = tourist.Cards,
        Score = tourist.Score,
      };

      Players = new List<PlayerHand>();
      foreach (var player in players)
      {
        Players.Add(new PlayerHand
        {
          Score = player.Score,
          PlayerId = player.PlayerId,
          PlayerCards = player.Cards
        });
      }

      SessionId = gameSessionId.ToString();
    }
  }
}

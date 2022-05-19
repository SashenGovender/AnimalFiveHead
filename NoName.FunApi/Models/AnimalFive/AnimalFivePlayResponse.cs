using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFiveHead.Player;

namespace NoName.FunApi.Models.AnimalFive
{
  public class AnimalFivePlayResponse
  {
    [JsonPropertyName("keeper")]
    public NpcBag? Keeper { get; init; }

    [JsonPropertyName("tourist")]
    public NpcBag? Tourist { get; init; }

    [JsonPropertyName("players")]
    public List<PlayerBag>? Players { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    public AnimalFivePlayResponse(KeeperPlayer keeper, TouristPlayer tourist, List<NormalPlayer> players, Guid gameSessionId)
    {
      Keeper = new NpcBag
      {
        Cards = keeper.Cards,
        Score = keeper.Score,
      };

      Tourist = new NpcBag
      {
        Cards = tourist.Cards,
        Score = tourist.Score,
      };

      Players = new List<PlayerBag>();
      foreach (var player in players)
      {
        Players.Add(new PlayerBag
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

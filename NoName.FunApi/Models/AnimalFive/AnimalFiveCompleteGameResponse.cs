using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFiveHead.Player;

namespace NoName.FunApi.Models.AnimalFive
{
  public class AnimalFiveCompleteGameResponse
  {
    [JsonPropertyName("tourist")]
    public NpcBag? Tourist { get; init; }

    [JsonPropertyName("keeper")]
    public NpcBag? Keeper { get; init; }

    [JsonPropertyName("players")]
    public List<PlayerBag>? Players { get; init; }

    [JsonPropertyName("sessionId")]
    public string? SessionId { get; init; }

    public AnimalFiveCompleteGameResponse(string? sessionId, KeeperPlayer keeper, TouristPlayer tourist, List<NormalPlayer> players)
    {
      SessionId = sessionId;

      Keeper = new NpcBag
      {
        Score = keeper.Score,
        Cards = keeper.Cards,
      };

      Tourist = new NpcBag
      {
        Score = tourist.Score,
        Cards = tourist.Cards,
      };

      Players = new List<PlayerBag>();
      foreach (var player in players)
      {
        Players.Add(new PlayerBag
        {
          PlayerId = player.PlayerId,
          Score = player.Score,
          Result = player.GameStatus,
          PlayerCards = player.Cards
        });
      }

    }
  }
}

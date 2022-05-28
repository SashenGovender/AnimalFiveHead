using System.Collections.Generic;
using System.Text.Json.Serialization;
using Game.AnimalFiveHead;

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

    public AnimalFiveCompleteGameResponse(string? sessionId, IAnimalFive animalFiveGame)
    {
      SessionId = sessionId;

      Keeper = new NpcBag(animalFiveGame.Keeper.Cards, animalFiveGame.Keeper.Score);
      Tourist = new NpcBag(animalFiveGame.Tourist.Cards, animalFiveGame.Tourist.Score);

      Players = new List<PlayerBag>();
      foreach (var player in animalFiveGame.Players)
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

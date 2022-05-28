using System.ComponentModel.DataAnnotations;

namespace NoName.FunApi.Models.AnimalFive
{
  public class AnimalFiveCompleteGameRequest
  {
    [Required]
    public string? SessionId { get; init; }
  }
}

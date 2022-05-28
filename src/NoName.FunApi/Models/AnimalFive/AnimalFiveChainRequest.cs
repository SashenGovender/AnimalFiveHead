using System.ComponentModel.DataAnnotations;

namespace NoName.FunApi.Models.AnimalFive
{
  public class AnimalFiveChainRequest
  {
    [Required]
    public string? SessionId { get; init; }

    [Required]
    public int PlayerId { get; init; }
  }
}

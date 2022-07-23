using System.ComponentModel.DataAnnotations;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class AnimalFiveHeadCompleteGameRequest
  {
    [Required]
    public string? SessionId { get; init; }
  }
}

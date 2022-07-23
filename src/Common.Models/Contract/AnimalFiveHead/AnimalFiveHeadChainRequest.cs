using System.ComponentModel.DataAnnotations;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class AnimalFiveHeadChainRequest
  {
    [Required]
    public string? SessionId { get; init; }

    [Required]
    public int PlayerId { get; init; }
  }
}

using System.ComponentModel.DataAnnotations;

namespace Common.Models.Contract.AnimalFiveHead
{
  public class AnimalFiveHeadPlayRequest
  {
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
    [Required]
    public int NumberOfPlayers { get; init; }
  }
}

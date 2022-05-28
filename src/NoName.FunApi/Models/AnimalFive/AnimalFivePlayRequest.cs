using System.ComponentModel.DataAnnotations;

namespace NoName.FunApi.Models.AnimalFive
{
  public class AnimalFivePlayRequest
  {
    //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-6.0
    [Required]
    public int NumberOfPlayers { get; init; }
  }
}

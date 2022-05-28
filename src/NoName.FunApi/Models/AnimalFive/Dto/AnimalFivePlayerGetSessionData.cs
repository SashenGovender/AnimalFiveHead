using System;

namespace NoName.FunApi.Models.AnimalFive.Dto
{
  public class AnimalFivePlayerGetSessionData
  {
    public Guid? SessionId { get; init; }
    public int PlayerId { get; init; }
    public string? CardIds { get; init; }
  }
}

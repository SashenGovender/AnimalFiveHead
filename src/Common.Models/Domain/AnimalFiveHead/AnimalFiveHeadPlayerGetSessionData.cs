using System;

namespace Common.Models.Domain.AnimalFiveHead
{
  public class AnimalFiveHeadPlayerGetSessionData
  {
    public Guid? SessionId { get; init; }
    public int PlayerId { get; init; }
    public string? CardIds { get; init; }
  }
}

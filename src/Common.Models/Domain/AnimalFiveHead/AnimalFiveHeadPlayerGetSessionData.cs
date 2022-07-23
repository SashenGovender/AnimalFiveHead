using System;

namespace Common.Models.Domain.AnimalFiveHead
{
  public class AnimalFivePlayerGetSessionData
  {
    public Guid? SessionId { get; init; }
    public int PlayerId { get; init; }
    public string? CardIds { get; init; }
  }
}

using System;
using Game.AnimalFiveHead.Enums;

namespace Common.Models.Domain.AnimalFiveHead
{
  public class AnimalFiveHeadPlayerSaveSessionData
  {
    public Guid? SessionId { get; init; }
    public int PlayerId { get; init; }
    public int Score { get; init; }
    public string? Cards { get; init; }
    public string? CardIds { get; init; }
    public PlayerMatchResult Result { get; init; }

  }
}

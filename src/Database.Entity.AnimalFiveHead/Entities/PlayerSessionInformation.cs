using System;

namespace Database.Entity.AnimalFiveHead.Models
{
  public class PlayerSessionInformation
  {
    public Guid SessionId { get; set; }
    public int PlayerId { get; set; }
    public int Score { get; set; }
    public string Cards { get; set; } = null!;
    public string CardIds { get; set; } = null!;
    public string GameSession { get; set; } = null!;
    public string GameResult { get; set; } = null!;
    public DateTime DateTimeAdded { get; set; }
    public DateTime? DateTimeUpdated { get; set; }
  }
}

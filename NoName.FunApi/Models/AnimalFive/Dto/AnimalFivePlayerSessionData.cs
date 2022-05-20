namespace NoName.FunApi.Models.AnimalFive.Dto
{
  public class AnimalFivePlayerSessionData
  {
    public string? SessionId { get; init; }
    public int PlayerId { get; init; }
    public int Score { get; init; }
    public string? Cards { get; init; }
    public string? CardIds { get; init; }

  }
}

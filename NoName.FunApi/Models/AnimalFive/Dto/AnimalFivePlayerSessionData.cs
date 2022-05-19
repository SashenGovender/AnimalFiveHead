namespace NoName.FunApi.Models.AnimalFive.Dto
{
  public class AnimalFivePlayerSessionData
  {
    public string SessionId { get; set; } = "";
    public int PlayerId { get; set; }
    public int Score { get; set; }
    public string PlayerCards { get; set; } = "";
    public string PlayerCardIds { get; set; } = "";

  }
}

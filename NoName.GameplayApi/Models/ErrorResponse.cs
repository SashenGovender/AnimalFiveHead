namespace NoName.GameplayApi.Models
{
  public class ErrorResponse
  {
    public int ErrorCode { get; private set; }
    public string Message { get; private set; }
    public string SourceApplication { get; private set; }

    public ErrorResponse(int errorCode, string message)
    {
      ErrorCode = errorCode;
      Message = message;
      SourceApplication = "AnimalFiveHead.GameplayApi";
    }
  }
}

using NoName.FunApi.Enums;

namespace NoName.FunApi.Models
{
  public class ErrorResponse
  {
    public ErrorCodes ErrorCode { get; init; }
    public string Message { get; init; }
    public string SourceApplication { get; init; }

    public ErrorResponse(ErrorCodes errorCode, string message)
    {
      ErrorCode = errorCode;
      Message = message;
      SourceApplication = "AnimalFiveHead.FunApi";
    }
  }
}

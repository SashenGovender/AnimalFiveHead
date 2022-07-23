using Common.Models.Enums;

namespace Common.Models.Contract
{
  public class ErrorResponse
  {
    public ErrorCodes ErrorCode { get; init; }
    public string Message { get; init; }
    public string SourceApplication { get; init; }

    public ErrorResponse(ErrorCodes errorCode, string message, string sourceApplication)
    {
      ErrorCode = errorCode;
      Message = message;
      SourceApplication = sourceApplication;
    }
  }
}

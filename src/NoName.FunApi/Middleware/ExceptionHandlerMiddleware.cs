using System;
using System.Net;
using System.Threading.Tasks;
using Common.Models.Contract;
using Common.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace NoName.FunApi.Middleware
{
  // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-6.0
  public class ExceptionHandlerMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        await HandleException(context, ex);
      }
    }

    private Task HandleException(HttpContext context, Exception ex)
    {
      var errorResponse = new ErrorResponse(ErrorCodes.Unknown, ex.Message, "NoName Fun Api V1");
      var result = JsonConvert.SerializeObject(errorResponse);

      _logger.LogError(result);

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      return context.Response.WriteAsync(result);
    }
  }
}

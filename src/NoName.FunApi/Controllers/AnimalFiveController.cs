using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Contract.AnimalFiveHead;
using Microsoft.AspNetCore.Mvc;
using NoName.FunApi.Enums;
using NoName.FunApi.Models;
using NoName.FunApi.Services;

namespace NoName.FunApi.Controllers
{

  [Route("api/animalfive")]
  [ApiController]
  public class AnimalFiveController : ControllerBase
  {
    private readonly IAnimalFiveHeadService _animalFiveService;

    public AnimalFiveController(IAnimalFiveHeadService animalFiveManager)
    {
      _animalFiveService = animalFiveManager;
    }

    [HttpPost("play")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Play([FromBody] AnimalFiveHeadPlayRequest request, CancellationToken token)
    {
      if (request.NumberOfPlayers <= 0)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.NegativeNumberOfPlayers, "Negative Number of Players"));
      }

      var playResponse = await _animalFiveService.BeginPlayAsync(request, token);

      return Ok(playResponse);
    }

    [HttpPost("chain")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Chain([FromBody] AnimalFiveHeadChainRequest request, CancellationToken token)
    {
      if (request.PlayerId < 0)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.NegativeNumberOfPlayers, "Cannot have Negative number of players"));
      }

      if (!Guid.TryParse(request.SessionId, out var sessionGuid))
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdIsNotAGuid, "Invalid Game Session Guid"));
      }

      var sessionExistsAndActive = await _animalFiveService.IsValidSessionGuid(sessionGuid, token);
      if (sessionExistsAndActive is false)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdDoesNotExistOrIsNotActive, "SessionId does not exist or is not active"));
      }

      var chainResponse = await _animalFiveService.ChainAsync(request, token);

      return Ok(chainResponse);
    }

    [HttpPost("complete")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Complete([FromBody] AnimalFiveHeadCompleteGameRequest request, CancellationToken token)
    {
      if (!Guid.TryParse(request.SessionId, out var sessionGuid))
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdIsNotAGuid, "Invalid Game Session Guid"));
      }

      var sessionExistsAndActive = await _animalFiveService.IsValidSessionGuid(sessionGuid, token);
      if (sessionExistsAndActive is false)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdDoesNotExistOrIsNotActive, "SessionId does not exist or is not active"));
      }

      var completeGameResponse = await _animalFiveService.CompleteGameAsync(request, token);

      return Ok(completeGameResponse);
    }

  }
}

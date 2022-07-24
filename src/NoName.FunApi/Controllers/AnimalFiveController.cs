using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Common.Models.Contract;
using Common.Models.Contract.AnimalFiveHead;
using Common.Models.Enums;
using Microsoft.AspNetCore.Mvc;
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

    /// <summary>
    /// Begin Playing the Game
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///     POST api/animalfive
    ///     {
    ///       "NumberOfPlayers": "2"
    ///     }.
    /// </remarks>
    /// <param name="request"> The PlayRequest with the number of players</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("play")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> Play([FromBody] AnimalFiveHeadPlayRequest request, CancellationToken token)
    {
      if (request.NumberOfPlayers <= 0)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.NegativeNumberOfPlayers, "Negative Number of Players", Constants.ApplicationName));
      }

      var playResponse = await _animalFiveService.BeginPlayAsync(request, token);

      return Ok(playResponse);
    }

    /// <summary>
    /// Chain the next player card
    /// </summary>
    /// <param name="request">The player request to chain</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("chain")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> Chain([FromBody] AnimalFiveHeadChainRequest request, CancellationToken token)
    {
      if (request.PlayerId < 0)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.NegativeNumberOfPlayers, "Cannot have Negative number of players", Constants.ApplicationName));
      }

      if (!Guid.TryParse(request.SessionId, out var sessionGuid))
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdIsNotAGuid, "Invalid Game Session Guid", Constants.ApplicationName));
      }

      var sessionExistsAndActive = await _animalFiveService.IsValidSessionGuid(sessionGuid, token);
      if (sessionExistsAndActive is false)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdDoesNotExistOrIsNotActive, "SessionId does not exist or is not active", Constants.ApplicationName));
      }

      var chainResponse = await _animalFiveService.ChainAsync(request, token);

      return Ok(chainResponse);
    }

    /// <summary>
    /// Complete the game and return the outcome of the game session
    /// </summary>
    /// <param name="request">The request to complete the game</param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpPost("complete")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<ActionResult> Complete([FromBody] AnimalFiveHeadCompleteGameRequest request, CancellationToken token)
    {
      if (!Guid.TryParse(request.SessionId, out var sessionGuid))
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdIsNotAGuid, "Invalid Game Session Guid", Constants.ApplicationName));
      }

      var sessionExistsAndActive = await _animalFiveService.IsValidSessionGuid(sessionGuid, token);
      if (sessionExistsAndActive is false)
      {
        return BadRequest(new ErrorResponse(ErrorCodes.SessionIdDoesNotExistOrIsNotActive, "SessionId does not exist or is not active", Constants.ApplicationName));
      }

      var completeGameResponse = await _animalFiveService.CompleteGameAsync(request, token);

      return Ok(completeGameResponse);
    }

  }
}

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoName.FunApi.GameManager;
using NoName.FunApi.Models;
using NoName.FunApi.Models.AnimalFive;

namespace NoName.FunApi.Controllers
{

  [Route("api/animalfive")]
  [ApiController]
  public class AnimalFiveController : ControllerBase
  {
    private readonly IAnimalFiveManager _animalFiveManager;

    public AnimalFiveController(IAnimalFiveManager animalFiveManager)
    {
      _animalFiveManager = animalFiveManager;
    }

    [HttpPost("play")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Play([FromBody] AnimalFivePlayRequest request, CancellationToken token)
    {
      if (request.NumberOfPlayers <= 0)
      {
        return BadRequest(new ErrorResponse(1, "Negative Number of Players"));
      }

      var playResponse = await _animalFiveManager.BeginPlayAsync(request, token);

      return Ok(playResponse);
    }

    [HttpPost("chain")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Chain([FromBody] AnimalFiveChainRequest request, CancellationToken token)
    {
      if (request.PlayerId < 0)
      {
        return BadRequest(new ErrorResponse(1, "Cannot have Negative number of players"));
      }

      if (!Guid.TryParse(request.SessionId, out _))
      {
        return BadRequest(new ErrorResponse(2, "Invalid Game Session Guid"));
      }

      var chainResponse = await _animalFiveManager.ChainAsync(request, token);

      return Ok(chainResponse);
    }

    [HttpPost("complete")]
    [Consumes("application/json")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> Complete([FromBody] AnimalFiveCompleteGameRequest request, CancellationToken token)
    {
      var completeGameResponse = await _animalFiveManager.CompleteGameAsync(request, token);

      return Ok(completeGameResponse);
    }

  }
}

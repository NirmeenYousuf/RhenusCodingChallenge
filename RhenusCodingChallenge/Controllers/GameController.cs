using Microsoft.AspNetCore.Mvc;
using RhenusCodingChallenge.Constants;
using RhenusCodingChallenge.GameState;
using RhenusCodingChallenge.Models;
using RhenusCodingChallenge.Services;

namespace RhenusCodingChallenge.Controllers;

[ApiController]
[Route("api/game")]
public class GameController: ControllerBase
{
    
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("{playerId}/bet")]
    public IActionResult PlaceBet(string playerId, [FromBody] BetRequest request)
    {
        if (request.Points <= 0 || request.Number < 0 || request.Number > 9)
        {
            return BadRequest(Messages.InvalidBet);
        }
        var result = _gameService.PlaceBet(playerId, request);
        if (result.Status == "error")
            return BadRequest(result.Message);

        return Ok(new {account = result.Account, status = result.Status, points = result.Points  });
    }
    
    [HttpPost("reset")]
    public IActionResult ResetGame()
    {
        _gameService.ResetGame();
        return Ok(Messages.GameResetSuccessful);
    }
    
   
}
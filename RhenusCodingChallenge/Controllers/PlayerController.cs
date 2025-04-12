using Microsoft.AspNetCore.Mvc;
using RhenusCodingChallenge.Constants;
using RhenusCodingChallenge.Models;
using RhenusCodingChallenge.Services;

namespace RhenusCodingChallenge.Controllers;

[ApiController]
[Route("api/player")]
public class PlayerController: ControllerBase
{
    
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpPost]
    public IActionResult CreatePlayer([FromBody] PlayerRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PlayerId))
            return BadRequest(Messages.PlayerIdRequired);

        bool success = _playerService.CreatePlayer(request.PlayerId);
        if (!success) return Conflict(Messages.PlayerAlreadyExists);
        return Ok(Messages.PlayerCreated);
    }

    [HttpGet("{playerId}")]
    public IActionResult GetPlayer(string playerId)
    {
        var player = _playerService.GetPlayer(playerId);
        if (player == null) return NotFound(Messages.PlayerNotFound);
        return Ok(player);
    }
    
    [HttpPost("{playerId}/reset")]
    public IActionResult ResetPlayer(string playerId)
    {
        var player = _playerService.GetPlayer(playerId);
        if (player == null) return NotFound(Messages.PlayerNotFound);
        _playerService.ResetPlayer(playerId);
        return Ok(Messages.PlayerAccountResetSuccessful);
    }
    
}
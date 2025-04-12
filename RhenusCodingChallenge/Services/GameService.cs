using RhenusCodingChallenge.Models;
using RhenusCodingChallenge.Repositories;
using Microsoft.Extensions.Logging;
using RhenusCodingChallenge.Constants;
using RhenusCodingChallenge.GameState;

namespace RhenusCodingChallenge.Services;

public class GameService: IGameService
{
    private readonly IPlayerRepository _repo;
    private readonly Random _random = new();
    private readonly IGameState _gameState;
    private readonly IPlayerService _playerService;

    public GameService(IPlayerRepository repo, IGameState gameState, IPlayerService playerService)
    {
        _repo = repo;
        _gameState = gameState;
        _playerService = playerService;
    }
    
    public BetResponse PlaceBet(string playerId, BetRequest request)
    {
        var player = _repo.GetPlayer(playerId);
        if (player == null)
        {
            return new BetResponse
            {
                Status = "error",
                Message = Messages.PlayerNotFound
            };
        }

        if (player.Account < request.Points)
        {
            return new BetResponse
            {
                Status = "error",
                Message = Messages.InsufficientBalance
            };
        }

        int generatedNumber = _gameState.CurrentNumber;
        bool isWinningBet = generatedNumber == request.Number;

        if (isWinningBet)
        {
            int reward = request.Points * 9;
            player.Account += reward;
            _repo.UpdatePlayer(player);
            return new BetResponse
            {
                Status = "won",
                Points = $"+{reward}",
                Account = player.Account
            };
        }
        else
        {
            player.Account -= request.Points;
            _repo.UpdatePlayer(player);
            return new BetResponse
            {
                Status = "lost",
                Points = $"-{request.Points}",
                Account = player.Account
            };
        }
    }

    public void ResetGame()
    {
        _gameState.GenerateNewNumber();
        _playerService.ResetAllAccounts();
    }
}
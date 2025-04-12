using RhenusCodingChallenge.Models;

namespace RhenusCodingChallenge.Services;

public interface IGameService
{
    BetResponse PlaceBet(string playerId, BetRequest request);
    void ResetGame();
}
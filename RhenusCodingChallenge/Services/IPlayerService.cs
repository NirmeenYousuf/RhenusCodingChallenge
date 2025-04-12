using RhenusCodingChallenge.Models;

namespace RhenusCodingChallenge.Services;

public interface IPlayerService
{
    Player? GetPlayer(string playerId);
    bool CreatePlayer(string playerId);
    void ResetPlayer(string playerId);
    void ResetAllAccounts();
}
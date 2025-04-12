using System.Collections.Concurrent;
using RhenusCodingChallenge.Models;

namespace RhenusCodingChallenge.Repositories;

public interface IPlayerRepository
{
    Player? GetPlayer(string playerId);
    ConcurrentDictionary<string, Player> GetAllPlayers();
    void AddPlayer(Player player);
    void UpdatePlayer(Player player);
    bool PlayerExists(string playerId);
    void ResetPlayerAccount(string playerId, int defaultAmount);
}
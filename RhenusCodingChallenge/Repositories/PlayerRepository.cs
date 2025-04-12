using System.Collections.Concurrent;
using RhenusCodingChallenge.Models;

namespace RhenusCodingChallenge.Repositories;

public class PlayerRepository: IPlayerRepository
{
    private readonly ConcurrentDictionary<string, Player> _players = new();

    public Player? GetPlayer(string playerId)
    {
       return _players.TryGetValue(playerId, out var player) ? player : null;
    }

    public ConcurrentDictionary<string, Player> GetAllPlayers()
    {
        return _players;
    }

    public void AddPlayer(Player player)
    {
        _players[player.PlayerId] = player;
    }

    public void UpdatePlayer(Player player)
    {
        _players[player.PlayerId] = player;
    }

    public bool PlayerExists(string playerId)
    {
        return _players.ContainsKey(playerId);
    }

    public void ResetPlayerAccount(string playerId, int defaultAmount)
    {
        _players[playerId].Account = defaultAmount;
    }
    
    
}
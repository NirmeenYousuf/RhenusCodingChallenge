using System.Collections.Concurrent;
using RhenusCodingChallenge.Constants;
using RhenusCodingChallenge.Models;
using RhenusCodingChallenge.Repositories;
using Player = RhenusCodingChallenge.Constants.Player;

namespace RhenusCodingChallenge.Services;

public class PlayerService: IPlayerService
{
    private readonly IPlayerRepository _repo;

    public PlayerService(IPlayerRepository repo)
    {
        _repo = repo;
    }

    public Models.Player? GetPlayer(string playerId)
    {
        return _repo.GetPlayer(playerId);
    }

    public bool CreatePlayer(string playerId)
    {
        if (_repo.PlayerExists(playerId)) return false;
        _repo.AddPlayer(new Models.Player { PlayerId = playerId, Account = Player.DefaultAccountValue});
        return true;
    }

    public void ResetPlayer(string playerId)
    {
        _repo.ResetPlayerAccount(playerId, Player.DefaultAccountValue);
    }
    
    public void ResetAllAccounts()
    {
        var players = _repo.GetAllPlayers();
        
        foreach (var player in players.Values)
        {
            ResetPlayer(player.PlayerId);
        }
        
    }
}
using System.Collections.Concurrent;
using FluentAssertions;
using Moq;
using RhenusCodingChallenge.Models;
using RhenusCodingChallenge.Repositories;
using RhenusCodingChallenge.Services;

namespace RhenusCodingChallenge.Tests.Services;

public class PlayerServiceTests
{
    private readonly Mock<IPlayerRepository> _repoMock;
    private readonly PlayerService _service;

    public PlayerServiceTests()
    {
        _repoMock = new Mock<IPlayerRepository>();
        _service = new PlayerService(_repoMock.Object);
    }

    [Fact]
    public void CreatePlayer_ShouldAddIfNotExists()
    {
        _repoMock.Setup(r => r.PlayerExists("test")).Returns(false);

        var result = _service.CreatePlayer("test");

        result.Should().BeTrue();
        _repoMock.Verify(r => r.AddPlayer(It.Is<Player>(p => p.PlayerId == "test")), Times.Once);
    }
    
    [Fact]
    public void CreatePlayer_ShouldNotAddIfExists()
    {
        _repoMock.Setup(r => r.PlayerExists("test")).Returns(true);

        var result = _service.CreatePlayer("test");

        result.Should().BeFalse();
        _repoMock.Verify(r => r.AddPlayer(It.Is<Player>(p => p.PlayerId == "test")), Times.Never);
    }

    [Fact]
    public void GetPlayer_ShouldReturnPlayer()
    {
        var expected = new Player { PlayerId = "1", Account = 10000 };
        _repoMock.Setup(r => r.GetPlayer("1")).Returns(expected);

        var result = _service.GetPlayer("1");

        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void ResetAllAccounts_ShouldResetPlayers()
    {
        var expected = new ConcurrentDictionary<string, Player>
        {
            ["1"] = new Player { PlayerId = "1", Account = 10000 },
            ["2"] = new Player { PlayerId = "2", Account = 10400 },
        };
        _repoMock.Setup(r => r.GetAllPlayers()).Returns(expected);
        _service.ResetAllAccounts();
        _repoMock.Verify(r => r.ResetPlayerAccount(It.IsAny<string>(), 10000), Times.Exactly(2));
    }
}
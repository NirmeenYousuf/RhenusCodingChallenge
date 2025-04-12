using FluentAssertions;
using RhenusCodingChallenge.Models;
using Moq;
using RhenusCodingChallenge.Services;
using RhenusCodingChallenge.Repositories;
using RhenusCodingChallenge.GameState;

namespace RhenusCodingChallenge.Tests.Services;

public class GameServiceTests
{
    
    private readonly Mock<IPlayerRepository> _playerRepoMock;
    private readonly Mock<IGameState> _gameStateMock;
    private readonly GameService _gameService;
    private readonly Mock<IPlayerService> _playerServiceMock;
    private readonly Player _testPlayer;

    public GameServiceTests()
    {
        _playerRepoMock = new Mock<IPlayerRepository>();
        _gameStateMock = new Mock<IGameState>();
        _playerServiceMock = new Mock<IPlayerService>();

        _gameService = new GameService(_playerRepoMock.Object, _gameStateMock.Object, _playerServiceMock.Object);

        _testPlayer = new Player { PlayerId = "test-player", Account = 10000 };
        _playerRepoMock.Setup(repo => repo.GetPlayer("test-player")).Returns(_testPlayer);
    }

    [Fact]
    public void PlaceBet_CorrectGuess_ShouldReturnWin()
    {
        _gameStateMock.Setup(s => s.CurrentNumber).Returns(5);

        var request = new BetRequest { Number = 5, Points = 100 };
        var result = _gameService.PlaceBet("test-player", request);

        result.Status.Should().Be("won");
        result.Points.Should().Be("+900");
        result.Account.Should().Be(10900);
    }

    [Fact]
    public void PlaceBet_WrongGuess_ShouldReturnLoss()
    {
        _gameStateMock.Setup(s => s.CurrentNumber).Returns(8);

        var request = new BetRequest { Number = 3, Points = 200 };
        var result = _gameService.PlaceBet("test-player", request);

        result.Status.Should().Be("lost");
        result.Points.Should().Be("-200");
        result.Account.Should().Be(9800);
    }
    
    [Fact]
    public void PlaceBet_InvalidPlayer_ShouldReturnError()
    {
        var request = new BetRequest { Number = 3, Points = 200 };
        var result = _gameService.PlaceBet("invalid-player", request);

        result.Status.Should().Be("error");
        result.Message.Should().Be("Player not found.");
    }
    
    [Fact]
    public void PlaceBet_InsufficientBalance_ShouldReturnError()
    {
        var request = new BetRequest { Number = 3, Points = 11000 };
        var result = _gameService.PlaceBet("test-player", request);

        result.Status.Should().Be("error");
        result.Message.Should().Be("Insufficient balance to place the bet. Please reset player's account and try again");
    }

    [Fact]
    public void ResetGame_ShouldResetGame()
    {
        _gameService.ResetGame();
        _gameStateMock.Verify(s => s.GenerateNewNumber(), Times.Once);
        _playerServiceMock.Verify(s =>s.ResetAllAccounts(), Times.Once);
    }
    
}
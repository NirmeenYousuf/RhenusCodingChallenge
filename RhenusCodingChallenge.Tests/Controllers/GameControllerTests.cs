using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using RhenusCodingChallenge.Models;

namespace RhenusCodingChallenge.Tests.Controllers;

public class GameControllerTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public GameControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Bet_ValidInput_ShouldReturnBetResponse()
    {
        await _client.PostAsJsonAsync("/api/player", new { playerId = "gamer" });

        var bet = new BetRequest { Points = 200, Number = 3 };
        var response = await _client.PostAsJsonAsync("/api/game/gamer/bet", bet);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<BetResponse>();
        result.Should().NotBeNull();
        result!.Status.Should().BeOneOf("won", "lost");
    }
    
    [Fact]
    public async Task Bet_InvalidPlayer_ShouldReturnBadRequest()
    {
        var bet = new BetRequest { Points = 100, Number = 5 };
        var response = await _client.PostAsJsonAsync("/api/game/invalid_player/bet", bet);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Bet_InvalidBet_ShouldReturnBadRequest()
    {
        var bet = new BetRequest { Points = 100, Number = 10 };
        var response = await _client.PostAsJsonAsync("/api/game/gamer/bet", bet);
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Invalid data provided for Bet. Points should be greater than 0 and number should be between 0 and 9");
    }
    
    [Fact]
    public async Task Reset_ShouldReturnSuccess()
    {
        var result = await _client.PostAsync("/api/game/reset", null);
        result.EnsureSuccessStatusCode();
    }
}
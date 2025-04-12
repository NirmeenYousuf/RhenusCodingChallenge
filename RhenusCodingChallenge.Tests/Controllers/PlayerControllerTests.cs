using System.Net.Http.Json;
using FluentAssertions;
using RhenusCodingChallenge.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace RhenusCodingChallenge.Tests.Controllers;

public class PlayerControllerTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PlayerControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreatePlayer_ShouldReturnSuccess()
    {
        var result = await _client.PostAsJsonAsync("/api/player", new { playerId = TestConstants.DefaultPlayerId });

        result.EnsureSuccessStatusCode();
        var content = await result.Content.ReadAsStringAsync();
        content.Should().Contain("Player created");
    }

    [Fact]
    public async Task GetPlayer_ShouldReturnCorrectPlayer()
    {
        await _client.PostAsJsonAsync("/api/player", new { playerId = "player1" });

        var response = await _client.GetAsync("/api/player/player1");
        response.EnsureSuccessStatusCode();

        var player = await response.Content.ReadFromJsonAsync<Player>();
        player!.PlayerId.Should().Be("player1");
        player.Account.Should().Be(TestConstants.DefaultAccount);
    }

    [Fact]
    public async Task GetPlayer_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/api/player/invalid_player");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
    }
}
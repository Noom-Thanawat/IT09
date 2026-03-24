using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
public class PostTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PostTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetPost_ShouldReturnOk()
    {
        var postId = "11111111-1111-1111-1111-111111111111";
        await _client.PostAsync("/api/posts/seed", null);
        var response = await _client.GetAsync($"/api/posts/{postId}");
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }

    [Fact]
    public async Task CreateComment_ShouldReturnBadRequest_WhenMessageEmpty()
    {
        var postId = "11111111-1111-1111-1111-111111111111";
        await _client.PostAsync("/api/posts/seed", null);

        var payload = new
        {
            authorName = "Test",
            message = ""
        };

        var response = await _client.PostAsJsonAsync($"/api/posts/{postId}/comments", payload);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task CreateComment_ShouldReturnOk_WhenValid()
    {
        var postId = "11111111-1111-1111-1111-111111111111";

        await _client.PostAsync("/api/posts/seed", null);

        var payload = new
        {
            authorName = "Test",
            message = "Hello"
        };

        var response = await _client.PostAsJsonAsync($"/api/posts/{postId}/comments", payload);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
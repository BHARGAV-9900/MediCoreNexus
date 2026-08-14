using System.Net;
using System.Net.Http.Json;
using MediCore.IntegrationTests.Infrastructure;

namespace MediCore.IntegrationTests.API;

public class AuthenticationTests
    : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthenticationTests(
        CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturnUnauthorized()
    {
        var request = new
        {
            email = "invalid@example.com",
            password = "WrongPassword123!"
        };

        var response = await _client.PostAsJsonAsync(
            "/api/v1/Authentication/login",
            request);

        Assert.Equal(
            HttpStatusCode.Unauthorized,
            response.StatusCode);
    }
}
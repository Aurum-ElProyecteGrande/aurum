namespace Aurum.IntegrationTests_v3.LoginTests;

using System.Net.Http.Json;
using Aurum.Data.Contracts;
using Aurum.IntegrationTests;
using Microsoft.AspNetCore.Mvc.Testing;

[Collection("IntegrationTests")]
public class AccountIntegrationTests : IClassFixture<AurumFactory>
{
    private readonly HttpClient _client;
    private readonly AurumFactory _factory;

    public AccountIntegrationTests(AurumFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
            
        _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = true
        });
    }

    [Fact]
    public async Task Login_As_Admin()
    {
        var loginResponse = await _client.PostAsJsonAsync("/User/Login", new AuthRequest("admin@admin.com", "admin123"));
        loginResponse.EnsureSuccessStatusCode();
            
        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
            
        Assert.NotEmpty(cookies);

        _client.DefaultRequestHeaders.Add("Cookie", cookies.First());
    }
}
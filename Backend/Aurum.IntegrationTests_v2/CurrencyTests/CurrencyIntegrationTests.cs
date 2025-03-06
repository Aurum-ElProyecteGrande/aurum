using System.Net;
using System.Net.Http.Json;
using Aurum.Models.CurrencyDtos;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Aurum.IntegrationTests;

[Collection("IntegrationTests")]
public class CurrencyIntegrationTests : IClassFixture<AurumFactory>
{
    private readonly HttpClient _client;

    public CurrencyIntegrationTests(AurumFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            HandleCookies = true
        });
    }

    [Fact]
    public async Task GetAllCurrencies_ShouldReturnListOfCurrencies()
    {
        var loginResponse = await _client.PostAsJsonAsync("/User/Login", new { Email = "admin@admin.com", Password = "admin123" });
        loginResponse.EnsureSuccessStatusCode();

        var cookies = loginResponse.Headers.GetValues("Set-Cookie");
        Assert.NotEmpty(cookies);
        _client.DefaultRequestHeaders.Add("Cookie", cookies.First());

        var response = await _client.GetAsync("/Currency");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var currencies = await response.Content.ReadFromJsonAsync<List<CurrencyDto>>();

        currencies.Should().NotBeNull();
        currencies.Should().NotBeEmpty();

        currencies.Should().Contain(c => c.CurrencyCode == "HUF" && c.Name == "Forint" && c.Symbol == "Ft");
        currencies.Should().Contain(c => c.CurrencyCode == "EUR" && c.Name == "Euro" && c.Symbol == "€");
        currencies.Should().Contain(c => c.CurrencyCode == "USD" && c.Name == "US Dollar" && c.Symbol == "$");
    }
}
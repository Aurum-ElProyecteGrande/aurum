using System.Net;
using System.Net.Http.Json;
using Aurum.Data.Context;
using Aurum.Data.Contracts;
using Aurum.Models.AccountDto;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aurum.IntegrationTests;

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
        public async Task CreateAccount_ShouldAddAccountToDatabase()
        {
            var loginResponse = await _client.PostAsJsonAsync("/User/Login", new AuthRequest("admin@admin.com", "admin123"));
            loginResponse.EnsureSuccessStatusCode();
            
            var newAccount = new ModifyAccountDto(
                DisplayName: "Test Account", 
                Amount: 100.50m,             
                CurrencyId: 1
            );
            
            var cookies = loginResponse.Headers.GetValues("Set-Cookie");
            
            Assert.NotEmpty(cookies);

            _client.DefaultRequestHeaders.Add("Cookie", cookies.First());
            
            var postResponse = await _client.PostAsJsonAsync("/Account", newAccount);
            postResponse.EnsureSuccessStatusCode();
            
            var createdAccountId = await postResponse.Content.ReadFromJsonAsync<int>();
            createdAccountId.Should().BeGreaterThan(0);
            
            var getResponse = await _client.GetAsync("/Account");
            getResponse.EnsureSuccessStatusCode(); 

            var accounts = await getResponse.Content.ReadFromJsonAsync<List<AccountDto>>();
            accounts.Should().NotBeEmpty();
            
            accounts.Should().Contain(a => 
                a.AccountId == createdAccountId &&
                a.DisplayName == newAccount.DisplayName &&
                a.Amount == newAccount.Amount
            );

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AurumContext>();

            var dbAccount = await dbContext.Accounts.FindAsync(createdAccountId);
            
            dbAccount.Should().NotBeNull();

            dbAccount!.DisplayName.Should().Be(newAccount.DisplayName);
            dbAccount.Amount.Should().Be(newAccount.Amount);
        }
    }

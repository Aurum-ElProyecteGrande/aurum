using Aurum.Data.Context;
using Aurum.Services.AccountService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Aurum.IntegrationTests;

public class AurumFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = Guid.NewGuid().ToString();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            var aurumDbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AurumContext>));

            services.Remove(aurumDbContextDescriptor);

            services.AddDbContext<AurumContext>(options =>
            {
                options.UseInMemoryDatabase(_dbName);
            });

            using var scope = services.BuildServiceProvider().CreateScope();

            var aurumContext = scope.ServiceProvider.GetRequiredService<AurumContext>();
            aurumContext.Database.EnsureDeleted();
            aurumContext.Database.EnsureCreated();

            var currencies = new List<Currency>()
            {
	            new Currency { CurrencyCode = "HUF", Name = "Forint", Symbol = "Ft" },
	            new Currency { CurrencyCode = "EUR", Name = "Euro", Symbol = "€" },
	            new Currency { CurrencyCode = "USD", Name = "US Dollar", Symbol = "$" }
            };

            aurumContext.Currencies.AddRangeAsync(currencies);
            aurumContext.SaveChangesAsync();

            // additional initialization for admin if needed
        });
    }
    }
}

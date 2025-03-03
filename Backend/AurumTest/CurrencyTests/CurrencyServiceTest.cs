using Aurum.Data.Entities;
using Aurum.Repositories.CurrencyRepository;
using Aurum.Services.CurrencyServices;
using Moq;

namespace AurumTest.Services.CurrencyServices;

[TestFixture]
[TestOf(typeof(CurrencyService))]
public class CurrencyServiceTest
{
    private Mock<ICurrencyRepo> _currencyRepoMock;
    private CurrencyService _currencyService;

    [SetUp]
    public void SetUp()
    {
        _currencyRepoMock = new Mock<ICurrencyRepo>();
        _currencyService = new CurrencyService(_currencyRepoMock.Object);
    }

    [Test]
    public async Task GetAll_ReturnsListOfCurrencies()
    {
        List<Currency> currencies = new List<Currency>
        {
            new Currency { CurrencyId = 1, Name = "USD" },
            new Currency { CurrencyId = 2, Name = "EUR" }
        };

        _currencyRepoMock.Setup(repo => repo.GetAll()).ReturnsAsync(currencies);

        var result = await _currencyService.GetAll();
        
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.Count, Is.EqualTo(currencies.Count));
            Assert.That(result, Is.EquivalentTo(currencies));
            _currencyRepoMock.Verify(repo => repo.GetAll(), Times.Once);
        });
    }
}
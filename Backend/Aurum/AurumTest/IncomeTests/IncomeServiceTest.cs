using Aurum.Data.Entities;
using Aurum.Repositories.IncomeRepository.IncomeRepository;
using Aurum.Services.IncomeServices;
using Moq;

namespace AurumTest.Services.IncomeServices;

[TestFixture]
[TestOf(typeof(IncomeService))]
public class IncomeServiceTest
{
    private Mock<IIncomeRepo> _incomeRepoMock;
    private IncomeService _incomeService;

    [SetUp]
    public void SetUp()
    {
        _incomeRepoMock = new Mock<IIncomeRepo>();
        _incomeService = new IncomeService(_incomeRepoMock.Object);
    }
    
    [Test]
    public void ValidateDates_ValidDates_ReturnsSameDates()
    {
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime endDate = new DateTime(2024, 12, 31);

        var result = _incomeService.ValidateDates(startDate, endDate);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Item1, Is.EqualTo(startDate));
            Assert.That(result.Item2, Is.EqualTo(endDate));
        });
    }

    [Test]
    public void ValidateDate_NullStartedDate_ThrowsArgumentNullException()
    {
        DateTime? startDate = null;
        DateTime endDate = new DateTime(2024, 12, 31);

        Assert.Throws<ArgumentNullException>(() => _incomeService.ValidateDates(startDate, endDate));
    }

    [Test]
    public void ValidateDates_NullEndDate_ThrowsArgumentNullException()
    {
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime? endDate = null;

        Assert.Throws<ArgumentNullException>(() => _incomeService.ValidateDates(startDate, endDate));
    }

    [Test]
    public void ValidateDates_BothNullDates_ThrowsArgumentNullException()
    {
        DateTime? startDate = null;
        DateTime? endDate = null;

        Assert.Throws<ArgumentNullException>(() => _incomeService.ValidateDates(startDate, endDate));
    }

    [Test]
    public async Task GetTotalIncome_ValidAccountId_ReturnsCorrectSum()
    {
        int accountId = 1;
        List<Income> incomes = new List<Income>
        {
            new Income { Amount = 100.50m },
            new Income { Amount = 200.75m },
            new Income { Amount = 50.25m }
        };
        decimal expected = 100.50m + 200.75m + 50.25m;

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId)).ReturnsAsync(incomes);

        var result = await _incomeService.GetTotalIncome(accountId);
        
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetTotalIncome_NoIncomes_ReturnsZero()
    {
        int accountId = 2;
        _incomeRepoMock.Setup(repo => repo.GetAll(accountId)).ReturnsAsync(new List<Income>());

        var result = await _incomeService.GetTotalIncome(accountId);
        
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public async Task GetTotalIncome_WithEndDate_ReturnsCorrectSum()
    {
        int accountId = 1;
        DateTime endDate = new DateTime(2024, 2, 1);
        
        List<Income> incomes = new List<Income>
        {
            new Income { Amount = 100.50m },
            new Income { Amount = 200.75m },
            new Income { Amount = 50.25m }
        };
        decimal expected = 100.50m + 200.75m + 50.25m;

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, endDate)).ReturnsAsync(incomes);

        var result = await _incomeService.GetTotalIncome(accountId, endDate);
        
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public async Task GetTotalIncome_WithEndDate_NoIncomes_ReturnsZero()
    {
        int accountId = 2;
        DateTime endDate = new DateTime(2024, 2, 1);

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, endDate)).ReturnsAsync(new List<Income>());

        var result = await _incomeService.GetTotalIncome(accountId, endDate);
        
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public async Task GetTotalIncome_WithEndDate_NullIncomeList_ReturnsZero()
    {
        int accountId = 3;
        DateTime endDate = new DateTime(2024, 2, 1);

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, endDate)).ReturnsAsync((List<Income>)null);

        var result = await _incomeService.GetTotalIncome(accountId, endDate);
        
        Assert.That(result, Is.EqualTo(0m));
    }

    [Test]
    public async Task GetAll_WithAccountId_ReturnsListOfIncomes()
    {
        int accountId = 1;
        List<Income> expectedIncomes = new List<Income>
        {
            new Income { IncomeId = 1, AccountId = accountId, Amount = 100 },
            new Income { IncomeId = 2, AccountId = accountId, Amount = 200 }
        };

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId)).ReturnsAsync(expectedIncomes);

        var result = await _incomeService.GetAll(accountId);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(expectedIncomes.Count));
            Assert.That(result[0].IncomeId, Is.EqualTo(expectedIncomes[0].IncomeId));
            Assert.That(result[1].Amount, Is.EqualTo(expectedIncomes[1].Amount));
        });
    }

    [Test]
    public async Task GetAll_WithAccountIdAndEndDate_ReturnsListOfIncomes()
    {
        int accountId = 1;
        DateTime endDate = new DateTime(2024, 12, 31);
        List<Income> expectedIncomes = new List<Income>
        {
            new Income { IncomeId = 1, AccountId = accountId, Amount = 100, Date = new DateTime(2024, 12, 31) },
            new Income() { IncomeId = 2, AccountId = accountId, Amount = 200, Date = new DateTime(2024, 12, 30) }
        };

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, endDate)).ReturnsAsync(expectedIncomes);

        var result = await _incomeService.GetAll(accountId, endDate);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(expectedIncomes.Count));
            Assert.That(result[0].IncomeId, Is.EqualTo(expectedIncomes[0].IncomeId));
            Assert.That(result[1].Amount, Is.EqualTo(expectedIncomes[1].Amount));
        });
    }

    [Test]
    public async Task ValidId_CreatesIncomeAndReturnsId()
    {
        int incomeId = 1;
        Income income = new Income { IncomeId = incomeId };

        _incomeRepoMock.Setup(repo => repo.Create(income)).ReturnsAsync(incomeId);

        var result = await _incomeService.Create(income);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(incomeId));
            _incomeRepoMock.Verify(repo => repo.Create(income), Times.Once);
        });
    }

    [Test]
    public async Task InvalidId_ThrowsInvalidOperationException_OnCreate()
    {
        int incomeId = 0;
        Income income = new Income { IncomeId = incomeId };

        _incomeRepoMock.Setup(repo => repo.Create(income)).ReturnsAsync(incomeId);
        
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _incomeService.Create(income));
    }

    [Test]
    public async Task ValidIncomeId_DeletesIncome_AndReturnsTrue()
    {
        int incomeId = 1;

        _incomeRepoMock.Setup(repo => repo.Delete(incomeId)).ReturnsAsync(true);

        var result = await _incomeService.Delete(incomeId);
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result);
            _incomeRepoMock.Verify(repo => repo.Delete(incomeId), Times.Once);
        });
    }

    [Test]
    public void NonExistingIncomeId_ThrowsInvalidOperationException()
    {
        int incomeId = 0;

        _incomeRepoMock.Setup(repo => repo.Delete(incomeId)).ReturnsAsync(false);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _incomeService.Delete(incomeId));
    }

    [Test]
    public async Task CorrectId_AndValidDates_ReturnsIncomes()
    {
        int accountId = 1;
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime endDate = new DateTime(2024, 12, 31);
        List<Income> incomes = new List<Income>
        {
            new Income { Amount = 100m, Label = "friendly loan"},
            new Income { Amount = 150m, Label = "inheritance"}
        };

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, startDate, endDate)).ReturnsAsync(incomes);

        var result = await _incomeService.GetAll(accountId, startDate, endDate);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EquivalentTo(incomes));
            Assert.That(result[0].Amount, Is.EqualTo(100m));
            Assert.That(result[1].Label, Is.EqualTo("inheritance"));
        });
    }

    [Test]
    public async Task RepositoryReturnsNull_ReturnsEmptyList()
    {
        int accountId = 1;
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime endDate = new DateTime(2024, 12, 31);

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, startDate, endDate)).ReturnsAsync(( List<Income>)null);

        var result = await _incomeService.GetAll(accountId, startDate, endDate);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }
    
    // test for case: GetAll(int accountId, DateTime startDate, DateTime endDate) gets invalid id throws argument exception
    
}
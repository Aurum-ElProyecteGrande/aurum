using Aurum.Data.Entities;
using Aurum.Models.IncomeDTOs;
using Aurum.Repositories.IncomeRepository.IncomeRepository;
using Aurum.Services.AccountService;
using Aurum.Services.IncomeCategoryServices;
using Aurum.Services.IncomeServices;
using Moq;

namespace AurumTest.Services.IncomeServices;

[TestFixture]
[TestOf(typeof(IncomeService))]
public class IncomeServiceTest
{
    private Mock<IIncomeRepo> _incomeRepoMock;
    private Mock<IAccountService> _accountServiceMock;
    private Mock<IIncomeCategoryService> _incomeCategoryServiceMock;

    private IncomeService _incomeService;
    
    [SetUp]
    public void SetUp()
    {
        _incomeRepoMock = new Mock<IIncomeRepo>();
        _accountServiceMock = new Mock<IAccountService>();
        _incomeCategoryServiceMock = new Mock<IIncomeCategoryService>();
        _incomeService = new IncomeService(_incomeRepoMock.Object, _incomeCategoryServiceMock.Object, _accountServiceMock.Object);
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
    public async Task GetAll_WithAccountId_ReturnsListOfIncomeDtos()
    {
        int accountId = 1;

        List<Income> mockIncomes = new List<Income>
        {
            new Income 
            { 
                IncomeId = 1, 
                AccountId = accountId, 
                IncomeCategoryId = 10, 
                Label = "Salary", 
                Amount = 1000, 
                Date = DateTime.Now
            },
            new Income 
            { 
                IncomeId = 2, 
                AccountId = accountId, 
                IncomeCategoryId = 20, 
                Label = "Bonus", 
                Amount = 500, 
                Date = DateTime.Now
            }
        };

        List<IncomeCategory> mockCategories = new()
        {
            new IncomeCategory { IncomeCategoryId = 10, Name = "Salary Category" },
            new IncomeCategory { IncomeCategoryId = 20, Name = "Bonus Category" }
        };
        
        _incomeRepoMock.Setup(repo => repo.GetAll(accountId)).ReturnsAsync(mockIncomes);

        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(mockCategories);

        var result = await _incomeService.GetAll(accountId);
        
        Assert.That(result, Is.Not.Null);

        Assert.That(result, Has.Count.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(result[0].Category.Name, Is.EqualTo("Salary Category"));
            Assert.That(result[0].Label, Is.EqualTo("Salary"));
            Assert.That(result[0].Amount, Is.EqualTo(1000));

            Assert.That(result[1].Category.Name, Is.EqualTo("Bonus Category"));
            Assert.That(result[1].Label, Is.EqualTo("Bonus"));
            Assert.That(result[1].Amount, Is.EqualTo(500));
        });
    }

    [Test]
    public async Task GetAll_WithNoIncomes_ReturnsEmptyList()
    {
        int accountId = 2;
        _incomeRepoMock.Setup(repo => repo.GetAll(accountId)).ReturnsAsync(new List<Income>());
        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(new List<IncomeCategory>());

        var result = await _incomeService.GetAll(accountId);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAll_WithAccountIdAndEndDate_ReturnsListOfIncomeDtos()
    {
        int accountId = 1;
        DateTime endDate = DateTime.UtcNow;
        
        List<Income> mockIncomes = new List<Income>
        {
            new Income { IncomeId = 1, AccountId = accountId, IncomeCategoryId = 10, Label = "Freelance", Amount = 200, Date = endDate }
        };

        List<IncomeCategory> mockCategories = new List<IncomeCategory>
        {
            new IncomeCategory { IncomeCategoryId = 10, Name = "Freelance Work" }
        };
    
        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, endDate)).ReturnsAsync(mockIncomes);
        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(mockCategories);
        
        var result = await _incomeService.GetAll(accountId, endDate);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Category.Name, Is.EqualTo("Freelance Work"));
            Assert.That(result[0].Label, Is.EqualTo("Freelance"));
            Assert.That(result[0].Amount, Is.EqualTo(200));
        });
    }

    [Test]
    public async Task GetAll_WithStartAndEndDate_ReturnsListOfIncomeDtos()
    {
        int accountId = 1;
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime endDate = new DateTime(2024, 12, 31);

        List<Income> mockIncomes = new List<Income>
        {
            new Income
            {
                IncomeId = 1, AccountId = accountId, IncomeCategoryId = 15, Label = "Consulting", Amount = 300,
                Date = new DateTime(2025, 6, 15)
            }
        };

        List<IncomeCategory> mockCategories = new List<IncomeCategory>
        {
            new IncomeCategory { IncomeCategoryId = 15, Name = "Consulting Services" }
        };

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, startDate, endDate)).ReturnsAsync(mockIncomes);
        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(mockCategories);

        var result = await _incomeService.GetAll(accountId, startDate, endDate);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Category.Name, Is.EqualTo("Consulting Services"));
            Assert.That(result[0].Label, Is.EqualTo("Consulting"));
            Assert.That(result[0].Amount, Is.EqualTo(300));
        });
    }

    [Test]
    public async Task Create_ValidModifyIncomeDto_ReturnsIncomeId()
    {
        ModifyIncomeDto newIncome = new(1, 5, "New Income", 600, DateTime.UtcNow);
        
        _incomeRepoMock.Setup(repo => repo.Create(It.IsAny<Income>())).ReturnsAsync(10);

        var result = await _incomeService.Create(newIncome);
        
        Assert.That(result, Is.EqualTo(10));
    }
    
    [Test]
    public async Task InvalidId_ThrowsInvalidOperationException_OnCreate()
    {
        ModifyIncomeDto modifyIncomeDto = new ModifyIncomeDto(1, 5, "New Income", 600, DateTime.UtcNow);
        
        _incomeRepoMock.Setup(repo => repo.Create(It.IsAny<Income>())).ReturnsAsync(0);

        var ex = Assert.ThrowsAsync<InvalidOperationException>(async () => await _incomeService.Create(modifyIncomeDto));
        
        Assert.That(ex.Message, Is.EqualTo("Invalid income input"));
    }

    [Test]
    public async Task Delete_InvalidIncomeId_ThrowsException()
    {
        int incomeId = 99;
        _incomeRepoMock.Setup(repo => repo.Delete(incomeId)).ReturnsAsync(false);

        var ex = Assert.ThrowsAsync<InvalidOperationException>(() => _incomeService.Delete(incomeId));
        Assert.That(ex.Message, Is.EqualTo($"Could not delete income with id {incomeId}"));
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
    public async Task RepositoryReturnsNull_ReturnsEmptyList()
    {
        int accountId = 1;
        DateTime startDate = new DateTime(2024, 1, 1);
        DateTime endDate = new DateTime(2024, 12, 31);

        _incomeRepoMock.Setup(repo => repo.GetAll(accountId, startDate, endDate)).ReturnsAsync(new List<Income>());

        var result = await _incomeService.GetAll(accountId, startDate, endDate);
        
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);
    }
}
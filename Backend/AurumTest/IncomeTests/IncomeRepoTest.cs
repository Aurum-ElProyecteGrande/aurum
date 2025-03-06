using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Repositories.IncomeRepository.IncomeRepository;
using Microsoft.EntityFrameworkCore;

[TestFixture]
public class IncomeRepoTest : IDisposable
{
    private IIncomeRepo _incomeRepo;
    private AurumContext _dbContext;

    [SetUp]
    public void SetUp()
    {
        // Use in-memory database for testing
        var options = new DbContextOptionsBuilder<AurumContext>()
            .UseInMemoryDatabase("AurumTestDb")
            .Options;

        _dbContext = new AurumContext(options);

        // Clear previous data and seed test data
        _dbContext.Incomes.RemoveRange(_dbContext.Incomes);
        _dbContext.SaveChanges();

        // Seed test data into in-memory database
        _dbContext.Incomes.AddRange(new List<Income>
        {
            new Income { IncomeId = 1, AccountId = 1, IncomeCategoryId = 1, Label = "Salary", Amount = 5000, Date = DateTime.UtcNow },
            new Income { IncomeId = 2, AccountId = 1, IncomeCategoryId = 2, Label = "Freelance", Amount = 1500, Date = DateTime.UtcNow.AddDays(-10) },
            new Income { IncomeId = 3, AccountId = 2, IncomeCategoryId = 1, Label = "Stocks", Amount = 2000, Date = DateTime.UtcNow.AddDays(-5) }
        });
        _dbContext.SaveChanges();

        _incomeRepo = new IncomeRepo(_dbContext);
    }

    /*[Test]
    public async Task GetAll_ReturnsOnlyIncomesForSpecificAccount()
    {
        var result = await _incomeRepo.GetAll(1);

        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result.All(income => income.AccountId == 1));
    }*/

    [Test]
    public async Task GetAll_ReturnsEmptyList_WhenNoMatchingAccount()
    {
        var result = await _incomeRepo.GetAll(999);

        Assert.That(result, Is.Empty);
    }

    /*[Test]
    public async Task GetAll_WithEndDate_ReturnsOnlyIncomesForSpecificAccount()
    {
        var result = await _incomeRepo.GetAll(1, DateTime.UtcNow.AddDays(-5));

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.All(income => income.AccountId == 1));
    }*/

    [Test]
    public async Task GetAll_WithEndDate_ReturnsEmptyList_WhenParametersDontMatch()
    {
        var result = await _incomeRepo.GetAll(1, DateTime.UtcNow.AddDays(-11));

        Assert.That(result.Count, Is.EqualTo(0));
    }

    /*[Test]
    public async Task GetAll_WithStartAndEndDate_ReturnsOnlyIncomesForSpecificAccount()
    {
        var result1 = await _incomeRepo.GetAll(1, DateTime.UtcNow.AddDays(-11), DateTime.UtcNow);
        var result2 = await _incomeRepo.GetAll(1, DateTime.UtcNow.AddDays(-9), DateTime.UtcNow);

        Assert.That(result1.Count, Is.EqualTo(2));
        Assert.That(result2.Count, Is.EqualTo(1));
    }*/

    [Test]
    public async Task GetAll_WithStartAndEndDate_ReturnsEmptyList_IfParametersDontMatch()
    {
        var result = await _incomeRepo.GetAll(1, DateTime.UtcNow.AddDays(-12), DateTime.UtcNow.AddDays(-11));

        Assert.That(result.Count, Is.EqualTo(0));
    }

    // public async Task<int> Create(Income income)
    // {
    //     await _dbContext.AddAsync(income);
    //     await _dbContext.SaveChangesAsync();
    //     return income.IncomeId;
    // }

    [Test]
    public async Task Create_AddsIncomeToDb_AndReturnsIncomeId()
    {
        int incomesCountBeforeCreate = _dbContext.Incomes.Count();

        var existingIncomeIds = _dbContext.Incomes.Select(i => i.IncomeId).ToList();

        Income incomeToAdd = new Income
        {
            AccountId = 1, IncomeCategoryId = 1, Label = "Lottery", Amount = 5000000,
            Date = DateTime.UtcNow
        };

        var result = await _incomeRepo.Create(incomeToAdd);

        Assert.That(_dbContext.Incomes.Count(), Is.EqualTo(incomesCountBeforeCreate + 1));
        Assert.That(existingIncomeIds.Contains(result), Is.False);
    }

    // // create method creates income even with invalid properties
    // // test case can be deleted, if unnecessary
    // [Test]
    // public async Task Create_ReturnsZero_WhenCreationFails()
    // {
    //     Income incomeToAdd = new Income
    //     {
    //         AccountId = -1, IncomeCategoryId = -1, Label = "Lottery", Amount = -5000000,
    //         Date = DateTime.UtcNow
    //     };
    //
    //     var result = await _incomeRepo.Create(incomeToAdd);
    //
    //     Assert.That(result, Is.EqualTo(0));
    // }

    [Test]
    public async Task DeleteRemovesIncomeFromDb_WithValidId_AndReturnsTrue()
    {
        int incomeIdToDelete = 2;
        int incomesCountBeforeDeletion = _dbContext.Incomes.Count();

        var result = await _incomeRepo.Delete(incomeIdToDelete);

        Assert.That(_dbContext.Incomes.Count(), Is.EqualTo(incomesCountBeforeDeletion - 1));
        Assert.True(result);
    }

    [Test]
    public async Task DeleteReturnsFalse_IfCantFindIncome()
    {
        int incomeIdToDelete = 99;
        int incomesCountBeforeDeletion = _dbContext.Incomes.Count();

        var result = await _incomeRepo.Delete(incomeIdToDelete);

        Assert.That(_dbContext.Incomes.Count(), Is.EqualTo(incomesCountBeforeDeletion));
        Assert.False(result);
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}

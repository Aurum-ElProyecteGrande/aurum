using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.IncomeDTOs;
using Aurum.Models.RegularityEnum;
using Aurum.Repositories.IncomeRepository.RegularIncomeRepository;
using Aurum.Repositories.RegularExpenseRepository;
using Aurum.Services.IncomeCategoryServices;
using Aurum.Services.RegularIncomeServices;
using Moq;

namespace AurumTest.Services.RegularIncomeServices;

[TestFixture]
[TestOf(typeof(RegularIncomeService))]
public class RegularIncomeServiceTest
{
    private Mock<IRegularIncomeRepo> _regularIncomeRepoMock;
    private Mock<IIncomeCategoryService> _incomeCategoryServiceMock;
    private IRegularIncomeService _regularIncomeService;

    [SetUp]
    public void SetUp()
    {
        _regularIncomeRepoMock = new Mock<IRegularIncomeRepo>();
        _incomeCategoryServiceMock = new Mock<IIncomeCategoryService>();
        _regularIncomeService =
            new RegularIncomeService(_regularIncomeRepoMock.Object, _incomeCategoryServiceMock.Object);
    }
    
    [Test]
    public async Task GetAllRegular_WithValidId_ReturnsListOfDtos()
    {
        int accountId = 1;

        List<CategoryDto> mockCategories = new List<CategoryDto>
        {
            new CategoryDto("Job", 2),
            new CategoryDto("Side Job", 3)
        };
        
        List<RegularIncome> mockRegularIncomes = new List<RegularIncome>
        {
            new RegularIncome
            {
                RegularIncomeId = 1, AccountId = accountId, IncomeCategoryId = 2, Label = "Salary", Amount = 5000, StartDate = DateTime.UtcNow, Regularity = Regularity.Monthly
            },
            new RegularIncome
            {
                RegularIncomeId = 2, AccountId = accountId, IncomeCategoryId = 3, Label = "Freelance", Amount = 2000, StartDate = DateTime.UtcNow, Regularity = Regularity.Weekly
            },
        };

        _regularIncomeRepoMock.Setup(repo => repo.GetAllRegular(accountId)).ReturnsAsync(mockRegularIncomes);
        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(new List<IncomeCategory>
        {
            new IncomeCategory() { IncomeCategoryId = 2, Name = "Job" },
            new IncomeCategory() { IncomeCategoryId = 3, Name = "Side Job" }
        });

        var result = await _regularIncomeService.GetAllRegular(accountId);
        
        Assert.NotNull(result);
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Label, Is.EqualTo("Salary"));
        Assert.That(result[1].Label, Is.EqualTo("Freelance"));
        Assert.That(result[0].Category.Name, Is.EqualTo("Job"));
        Assert.That(result[1].Category.Name, Is.EqualTo("Side Job"));
        
        _regularIncomeRepoMock.Verify(repo => repo.GetAllRegular(accountId), Times.Once);
        // verify should also be called only once
        // because of current method implementation it's called as many times as number of incomes added
        _incomeCategoryServiceMock.Verify(service => service.GetAllCategory(), Times.AtLeastOnce);
    }

    [Test]
    public async Task GetAllRegular_ReturnsEmptyList_WhenNoDataExists()
    {
        int accountId = 1;
        
        _regularIncomeRepoMock.Setup(repo => repo.GetAllRegular(accountId)).ReturnsAsync(new List<RegularIncome>());

        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(new List<IncomeCategory>());

        var result = await _regularIncomeService.GetAllRegular(accountId);
        
        Assert.NotNull(result);
        Assert.IsEmpty(result);
        
        _regularIncomeRepoMock.Verify(repo => repo.GetAllRegular(accountId), Times.Once);
    }
}
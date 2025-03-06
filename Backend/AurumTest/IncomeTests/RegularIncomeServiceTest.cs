using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;
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

    /*[Test]
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
        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(new List<CategoryDto>
        {
            new CategoryDto("Job", 2),
            new CategoryDto("Side Job", 3)
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
    }*/

    [Test]
    public async Task GetAllRegular_ReturnsEmptyList_WhenNoDataExists()
    {
        int accountId = 1;

        _regularIncomeRepoMock.Setup(repo => repo.GetAllRegular(accountId)).ReturnsAsync(new List<RegularIncome>());

        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(new List<CategoryDto>());

        var result = await _regularIncomeService.GetAllRegular(accountId);

        Assert.NotNull(result);
        Assert.IsEmpty(result);

        _regularIncomeRepoMock.Verify(repo => repo.GetAllRegular(accountId), Times.Once);
    }

    [Test]
    public async Task CreateRegular_WithValidIncome_ReturnsRegularIncomeId()
    {
        var modifyIncomeDto = new ModifyRegularIncomeDto(1,
            2,
            "Salary",
            5000,
            DateTime.UtcNow,
            Regularity.Monthly);

        var expectedRegularIncome = new RegularIncome()
        {
            AccountId = modifyIncomeDto.AccountId,
            IncomeCategoryId = modifyIncomeDto.CategoryId,
            Label = modifyIncomeDto.Label,
            Amount = modifyIncomeDto.Amount,
            StartDate = modifyIncomeDto.StartDate,
            Regularity = modifyIncomeDto.Regularity
        };

        _regularIncomeRepoMock.Setup(repo => repo.CreateRegular(It.IsAny<RegularIncome>())).ReturnsAsync(1);

        var result = await _regularIncomeService.CreateRegular(modifyIncomeDto);

        Assert.That(result, Is.EqualTo(1));
        _regularIncomeRepoMock.Verify(repo => repo.CreateRegular(It.IsAny<RegularIncome>()), Times.Once);
    }

    [Test]
    public void CreateRegular_WithInvalidIncome_ThrowsInvalidOperationException()
    {
        var modifyIncomeDto = new ModifyRegularIncomeDto(
            1,
            2,
            "Salary",
            5000,
            DateTime.UtcNow,
            Regularity.Monthly);

        _regularIncomeRepoMock.Setup(repo => repo.CreateRegular(It.IsAny<RegularIncome>())).ReturnsAsync(0);

        var exception = Assert.ThrowsAsync<InvalidOperationException>(() => _regularIncomeService.CreateRegular(modifyIncomeDto));
        Assert.That(exception.Message, Is.EqualTo("Invalid regular income input"));
        _regularIncomeRepoMock.Verify(repo => repo.CreateRegular(It.IsAny<RegularIncome>()), Times.Once);
    }

    [Test]
    public async Task DeleteRegularWithValidId_ReturnsTrue_WhenSuccessful()
    {
        int regularIncomeId = 1;

        _regularIncomeRepoMock.Setup(repo => repo.DeleteRegular(regularIncomeId)).ReturnsAsync(true);

        var result = await _regularIncomeService.DeleteRegular(regularIncomeId);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteRegular_ThrowsInvalidOperationException_WhenUnsuccessful()
    {
        int regularIncomeId = 0;

        _regularIncomeRepoMock.Setup(repo => repo.DeleteRegular(regularIncomeId)).ReturnsAsync(false);

        var exception = Assert.ThrowsAsync<InvalidOperationException>(() => _regularIncomeService.DeleteRegular(regularIncomeId));

        Assert.That(exception.Message, Is.EqualTo($"Could not delete income with id {regularIncomeId}"));

        _regularIncomeRepoMock.Verify(repo => repo.DeleteRegular(regularIncomeId), Times.Once);
    }

    /*[Test]
    public async Task ConvertRegularIncomeToDto_ReturnsDto_WithValidRegularIncome_AsInput()
    {
        var regularIncome = new RegularIncome
        {
            AccountId = 1,
            Amount = 2000,
            IncomeCategoryId = 2,
            Label = "Side Job",
            RegularIncomeId = 1,
            Regularity = Regularity.Weekly,
            StartDate = DateTime.UtcNow
        };

        var mockCategories = new List<CategoryDto>
        {
	        new CategoryDto("Job", 2),
	        new CategoryDto("Side Job", 3)
        };

        _incomeCategoryServiceMock.Setup(service => service.GetAllCategory()).ReturnsAsync(mockCategories);

        var expectedDto = new RegularIncomeDto(
            regularIncome.RegularIncomeId,
            new CurrencyDto("Forint", "HUF", "Ft"),
            new CategoryDto("Job", 2),
            regularIncome.Label,
            regularIncome.Amount,
            regularIncome.StartDate,
            regularIncome.Regularity
        );

        var result = await InvokeConvertRegularIncomeToDto(regularIncome);

        Assert.NotNull(result);
        Assert.That(result.RegularId, Is.EqualTo(expectedDto.RegularId));
        Assert.That(result.Currency, Is.EqualTo(expectedDto.Currency));
        Assert.That(result.Category.Name, Is.EqualTo(expectedDto.Category.Name));
        Assert.That(result.Category.CategoryId, Is.EqualTo(expectedDto.Category.CategoryId));
        Assert.That(result.Label, Is.EqualTo(expectedDto.Label));
        Assert.That(result.Amount, Is.EqualTo(expectedDto.Amount));
        Assert.That(result.StartDate, Is.EqualTo(expectedDto.StartDate));
        Assert.That(result.Regularity, Is.EqualTo(expectedDto.Regularity));

        _incomeCategoryServiceMock.Verify(service => service.GetAllCategory(), Times.Once);
    }*/

    private async Task<RegularIncomeDto> InvokeConvertRegularIncomeToDto(RegularIncome income)
    {
        var method = typeof(RegularIncomeService).GetMethod("ConvertRegularIncomeToDto", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return await (Task<RegularIncomeDto>)method.Invoke(_regularIncomeService, new object[] { income });
    }
}

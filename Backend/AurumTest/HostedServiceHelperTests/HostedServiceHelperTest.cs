using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;
using Aurum.Models.ExpenseDto;
using Aurum.Models.IncomeDTOs;
using Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;
using Aurum.Services.AccountService;
using Aurum.Services.ExpenseService;
using Aurum.Services.HostedHelperServices;
using Aurum.Services.IncomeServices;
using Aurum.Services.RegularExpenseService;
using Aurum.Services.RegularIncomeServices;
using Aurum.Services.UserServices;

using Moq;

namespace AurumTest.HostedServiceHelperTests;

[TestFixture]
public class HostedServiceHelperTests
{
	private Mock<IRegularExpenseService> _mockRegularExpenseService;
	private Mock<IRegularIncomeService> _mockRegularIncomeService;
	private Mock<IExpenseService> _mockExpenseService;
	private Mock<IIncomeService> _mockIncomeService;
	private HostedServiceHelper _helper;

	[SetUp]
	public void SetUp()
	{
		_mockRegularExpenseService = new Mock<IRegularExpenseService>();
		_mockRegularIncomeService = new Mock<IRegularIncomeService>();
		_mockExpenseService = new Mock<IExpenseService>();
		_mockIncomeService = new Mock<IIncomeService>();

		_helper = new HostedServiceHelper(
			_mockRegularExpenseService.Object,
			_mockExpenseService.Object,
			_mockRegularIncomeService.Object,
			_mockIncomeService.Object
		);
	}

	[Test]
	public async Task ProcessRegularToNormal_ShouldCreateExpensesAndIncomes_WhenFilteredDataExists()
	{
		// Arrange
		var mockExpenses = new List<RegularExpense>
		{
			new RegularExpense
			{
				RegularExpenseId = 1,
				AccountId = 1,
				Label = "Expense 1",
				Amount = 100,
				StartDate = DateTime.Today,
				Regularity = Regularity.Daily
			},
			new RegularExpense
			{
				RegularExpenseId = 2,
				AccountId = 1,
				Label = "Expense 2",
				Amount = 200,
				StartDate = DateTime.Today.AddDays(1),
				Regularity = Regularity.Weekly
			}
		};

		var mockIncomes = new List<RegularIncome>
		{
			new RegularIncome
			{
				RegularIncomeId = 1,
				AccountId = 1,
				Label = "Income 1",
				Amount = 1000,
				StartDate = DateTime.Today,
				Regularity = Regularity.Daily
			},
			new RegularIncome
			{
				RegularIncomeId = 2,
				AccountId = 1,
				Label = "Income 2",
				Amount = 1500,
				StartDate = DateTime.Today.AddDays(1),
				Regularity = Regularity.Weekly
			}
		};

		_mockRegularExpenseService
			.Setup(x => x.GetAll())
			.ReturnsAsync(mockExpenses);

		_mockRegularIncomeService
			.Setup(x => x.GetAllRegular())
			.ReturnsAsync(mockIncomes);

		_mockExpenseService
			.Setup(x => x.CreateRangeFromRegular(It.IsAny<List<RegularExpense>>()))
			.ReturnsAsync(true);

		_mockIncomeService
			.Setup(x => x.CreateRangeFromRegular(It.IsAny<List<RegularIncome>>()))
			.ReturnsAsync(true);

		// Act
		var result = await _helper.ProcessRegularToNormal();

		Assert.That(result, Is.True);
		_mockExpenseService.Verify(x => x.CreateRangeFromRegular(It.Is<List<RegularExpense>>(e => e.Count == 1)),
			Times.Once);
		_mockIncomeService.Verify(x => x.CreateRangeFromRegular(It.Is<List<RegularIncome>>(e => e.Count == 1)),
			Times.Once);
	}

	[Test]
	public async Task ProcessRegularToNormal_ShouldReturnFalse_WhenCreationFails()
	{
		// Arrange
		var mockExpenses = new List<RegularExpense>
		{
			new RegularExpense
			{
				RegularExpenseId = 1,
				AccountId = 1,
				Label = "Expense 1",
				Amount = 100,
				StartDate = DateTime.Today,
				Regularity = Regularity.Daily
			}
		};

		var mockIncomes = new List<RegularIncome>
		{
			new RegularIncome
			{
				RegularIncomeId = 1,
				AccountId = 1,
				Label = "Income 1",
				Amount = 1000,
				StartDate = DateTime.Today,
				Regularity = Regularity.Daily
			}
		};

		_mockRegularExpenseService
			.Setup(x => x.GetAll())
			.ReturnsAsync(mockExpenses);

		_mockRegularIncomeService
			.Setup(x => x.GetAllRegular())
			.ReturnsAsync(mockIncomes);

		_mockExpenseService
			.Setup(x => x.CreateRangeFromRegular(It.IsAny<List<RegularExpense>>()))
			.ReturnsAsync(false);

		_mockIncomeService
			.Setup(x => x.CreateRangeFromRegular(It.IsAny<List<RegularIncome>>()))
			.ReturnsAsync(true);

		// Act
		var result = await _helper.ProcessRegularToNormal();

		// Assert
		Assert.That(result, Is.False);
		_mockExpenseService.Verify(x => x.CreateRangeFromRegular(It.Is<List<RegularExpense>>(e => e.Count == 1)),
			Times.Once);
		_mockIncomeService.Verify(x => x.CreateRangeFromRegular(It.Is<List<RegularIncome>>(e => e.Count == 1)),
			Times.Once);
	}
}

using Aurum.Data.Entities;
using Aurum.Models.ExpenseDto;
using Aurum.Models.IncomeDTOs;
using Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;
using Aurum.Services.AccountService;
using Aurum.Services.ExpenseService;
using Aurum.Services.IncomeServices;
using Aurum.Services.RegularExpenseService;
using Aurum.Services.RegularIncomeServices;
using Aurum.Services.UserServices;

namespace Aurum.Services.HostedHelperServices;

public class HostedServiceHelper(
	IRegularExpenseService regularExpenseService,
	IExpenseService expenseService,
	IRegularIncomeService regularIncomeService,
	IIncomeService incomeService) : IHostedServiceHelper
{
	public async Task<bool> ProcessRegularToNormal()
	{

		var expenses = await regularExpenseService.GetAll();
		var incomes = await regularIncomeService.GetAllRegular();

		var filteredExpenses = expenses
			.Where(r =>
				(r.Regularity == Regularity.Monthly && r.StartDate.Day == DateTime.Today.Day) ||
				(r.Regularity == Regularity.Daily) ||
				(r.Regularity == Regularity.Weekly && r.StartDate.DayOfWeek == DateTime.Today.DayOfWeek))
			.ToList();

		var filteredIncomes = incomes
			.Where(r =>
				(r.Regularity == Regularity.Monthly && r.StartDate.Day == DateTime.Today.Day) ||
				(r.Regularity == Regularity.Daily) ||
				(r.Regularity == Regularity.Weekly && r.StartDate.DayOfWeek == DateTime.Today.DayOfWeek))
			.ToList();

		var didCreateExpenses = await expenseService.CreateRangeFromRegular(filteredExpenses);
		var didCreateIncomes = await incomeService.CreateRangeFromRegular(filteredIncomes);

		return didCreateExpenses && didCreateIncomes;
	}
}

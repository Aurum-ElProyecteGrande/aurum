using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;

namespace Aurum.Services.ExpenseService;

public interface IExpenseService
{
	Task<List<ExpenseDto>> GetAll(int accountId);
	Task<List<ExpenseDto>> GetAll(int accountId, DateTime startDate, DateTime endDate);
	Task<List<ExpenseWithCurrency>> GetAllWithCurrency(int accountId);
	Task<int> Create(ModifyExpenseDto expense);
	Task<bool> Delete(int expenseId);
	Task<decimal> GetTotalExpense(int accountId);
	Task<decimal> GetTotalExpense(int accountId, DateTime date);
}
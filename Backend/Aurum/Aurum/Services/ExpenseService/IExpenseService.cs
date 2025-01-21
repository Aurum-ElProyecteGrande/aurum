using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;

namespace Aurum.Services.ExpenseService;

public interface IExpenseService
{
	Task<List<ExpenseDto>> GetAll(int accountId, int userId);
	Task<List<ExpenseDto>> GetAll(int accountId, int userId, DateTime startDate, DateTime endDate);
	Task<int> Create(ModifyExpenseDto expense);
}
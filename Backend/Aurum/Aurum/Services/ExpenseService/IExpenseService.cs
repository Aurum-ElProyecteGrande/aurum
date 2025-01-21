using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;

namespace Aurum.Services.ExpenseService;

public interface IExpenseService
{
	Task<List<ExpenseDto>> GetAll(int accountId, int userID);
	Task<List<ExpenseDto>> GetAll(int accountId, int userID, DateTime startDate, DateTime endDate);
	Task<int> Create(ModifyExpenseDto expense);
}
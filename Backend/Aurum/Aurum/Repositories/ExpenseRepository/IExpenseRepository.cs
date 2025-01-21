using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;

namespace Aurum.Repositories.ExpenseRepository;

public interface IExpenseRepository
{
	Task<List<RawExpenseDto>> GetAll(int accountId);
	Task<List<RawExpenseDto>> GetAll(int accountId, DateTime startDate, DateTime endDate);
	Task<int> Create(ModifyExpenseDto expense);
	Task<bool> Delete(int expenseId);
}
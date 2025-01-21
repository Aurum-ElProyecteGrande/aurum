using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;

namespace Aurum.Repositories.ExpenseRepository;

public interface IExpenseRepository
{
	List<RawExpenseDto> GetAll(int accountId);
	List<ExpenseDto> GetAll(int accountId, DateTime startDate, DateTime endDate);
	int Create(ModifyExpenseDto expense);
	bool Delete(int expenseId);
}
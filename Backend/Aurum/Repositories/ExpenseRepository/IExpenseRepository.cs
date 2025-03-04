using Aurum.Data.Entities;
using Aurum.Models.ExpenseDto;

namespace Aurum.Repositories.ExpenseRepository;

public interface IExpenseRepository
{
	Task<List<Expense>> GetAll(int accountId);
	Task<List<Expense>> GetAll(int accountId, DateTime endDate);
	Task<List<Expense>> GetAll(int accountId, DateTime startDate, DateTime endDate);
	Task<int> Create(Expense expense);
	Task<bool> Delete(int expenseId);
}
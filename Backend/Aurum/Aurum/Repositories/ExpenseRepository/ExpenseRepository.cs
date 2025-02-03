using Aurum.Models.ExpenseDto;

namespace Aurum.Repositories.ExpenseRepository;

public class ExpenseRepository: IExpenseRepository
{
	public Task<List<Expense>> GetAll(int accountId)
	{
		throw new NotImplementedException();
	}

	public Task<List<Expense>> GetAll(int accountId, DateTime endDate)
	{
		throw new NotImplementedException();
	}

	public Task<List<Expense>> GetAll(int accountId, DateTime startDate, DateTime endDate)
	{
		throw new NotImplementedException();
	}

	public Task<int> Create(Expense expense)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Delete(int expenseId)
	{
		throw new NotImplementedException();
	}
}
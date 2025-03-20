using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.ExpenseDto;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Repositories.ExpenseRepository;

public class ExpenseRepository(AurumContext aurumContext): IExpenseRepository
{
	private AurumContext _context = aurumContext;

	public async Task<List<Expense>> GetAll(int accountId) =>
		await _context.Expenses
			.Where(e => e.AccountId == accountId)
			.Include(e => e.ExpenseCategory)
			.Include(e => e.ExpenseSubCategory)
			.Include(e => e.Account)
				.ThenInclude(a => a.Currency)
			.ToListAsync();

	public async Task<List<Expense>> GetAll(int accountId, DateTime endDate) =>
		await _context.Expenses
			.Where(e => e.AccountId == accountId
			            && e.Date <= endDate)
			.Include(e => e.ExpenseCategory)
			.Include(e => e.ExpenseSubCategory)
			.Include(e => e.Account)
				.ThenInclude(a => a.Currency)
			.ToListAsync();


	public async Task<List<Expense>> GetAll(int accountId, DateTime startDate, DateTime endDate) =>
		await _context.Expenses
			.Where(e => e.AccountId == accountId &&
			            e.Date >= startDate &&
			            e.Date <= endDate)
			.Include(e => e.ExpenseCategory)
			.Include(e => e.ExpenseSubCategory)
			.Include(e => e.Account)
				.ThenInclude(a => a.Currency)
			.ToListAsync();

	public async Task<int> Create(Expense expense)
	{
		_context.Expenses.Add(expense);
		await _context.SaveChangesAsync();

		return expense.ExpenseId;
	}

	public async Task<bool> Delete(int expenseId)
	{
		try
		{
			var expense = new Expense() { ExpenseId = expenseId };

			_context.Entry(expense).State = EntityState.Deleted;

			await _context.SaveChangesAsync();

			return true;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new KeyNotFoundException("Expense not found");
		}
	}

	public async Task<bool> CreateRange(List<Expense> expenses)
	{
		_context.Expenses.AddRange(expenses);
		var result = await _context.SaveChangesAsync();

		return result > 0;
	}
}

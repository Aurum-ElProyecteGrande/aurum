using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.RegularExpenseDto;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Repositories.RegularExpenseRepository;

public class RegularExpenseRepository(AurumContext aurumContext) : IRegularExpenseRepository
{
	private AurumContext _context = aurumContext;

	public async Task<List<RegularExpense>> GetAllRegular(int accountId) =>
		await _context.RegularExpenses
			.Where(r => r.AccountId == accountId)
			.ToListAsync();

	public async Task<int> Create(RegularExpense expense)
	{
		_context.RegularExpenses.Add(expense);
		await _context.SaveChangesAsync();

		return expense.RegularExpenseId;
	}

	public async Task<int> Update(RegularExpense expense)
	{
		try
		{
			_context.RegularExpenses.Update(expense);
			await _context.SaveChangesAsync();

			return expense.RegularExpenseId;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw new KeyNotFoundException("Expense not found");
		}
	}

	public async Task<bool> Delete(int regularId)
	{
		try
		{
			var expense = new RegularExpense() { RegularExpenseId = regularId };
			
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
}
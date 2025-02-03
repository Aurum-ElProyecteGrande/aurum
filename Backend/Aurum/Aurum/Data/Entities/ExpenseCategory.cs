using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Data.Entities;

public class ExpenseCategory
{
	public int ExpenseCategoryId { get; set; }
	public string Name { get; set; }
	
	public ICollection<ExpenseSubCategory> ExpenseSubCategories { get; set; } = [];
	public ICollection<Expense> Expenses { get; set; } = [];
	public ICollection<RegularExpense> RegularExpenses { get; set; } = [];
}
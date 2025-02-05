using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Data.Entities;

public class ExpenseSubCategory
{
	public int ExpenseSubCategoryId { get; set; }
	public int ExpenseCategoryId { get; set; }
	public string Name { get; set; }
	public bool IsBase { get; set; } = false;
	public int? UserId { get; set; } = null;
	
	public ICollection<Expense> Expenses { get; set; } = [];
	public ICollection<RegularExpense> RegularExpenses { get; set; } = [];
}
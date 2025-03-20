using Aurum.Models.RegularityEnum;

namespace Aurum.Data.Entities;

public class RegularExpense
{
	public int RegularExpenseId { get; set; }
	public int AccountId { get; set; }
	public Account Account { get; set; }
	public int ExpenseCategoryId { get; set; }
	public ExpenseCategory ExpenseCategory { get; set; }
	public int? ExpenseSubCategoryId { get; set; } = null;
	public ExpenseSubCategory ExpenseSubCategory { get; set; }
	public string Label { get; set; }
	public decimal Amount { get; set; }
	public DateTime StartDate { get; set; }
	public Regularity Regularity { get; set; }
}

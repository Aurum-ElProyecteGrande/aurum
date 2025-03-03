namespace Aurum.Data.Entities;

public class Expense
{
	public int ExpenseId { get; set; }
	public int AccountId { get; set; }
	public Account Account { get; set; }
	public int ExpenseCategoryId { get; set; }
	public ExpenseCategory ExpenseCategory { get; set; }
	public int? ExpenseSubCategoryId { get; set; } = null;
	public ExpenseSubCategory ExpenseSubCategory { get; set; }
	public string Label { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
namespace Aurum.Models.ExpenseDto;

public class Expense
{
	public int ExpenseId { get; set; }
	public int AccountId { get; set; }
	public int ExpenseCategoryId { get; set; }
	public int? ExpenseSubCategoryId { get; set; } = null;
	public string Label { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
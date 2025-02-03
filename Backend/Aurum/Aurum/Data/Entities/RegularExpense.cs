using Aurum.Models.RegularityEnum;

namespace Aurum.Models.RegularExpenseDto;

using Aurum.Models.RegularityEnum;

public class RegularExpense
{
	public int RegularExpenseId { get; set; }
	public int AccountId { get; set; }
	public int ExpenseCategoryId { get; set; }
	public int? ExpenseSubcategoryId { get; set; } = null;
	public string Label { get; set; }
	public decimal Amount { get; set; }
	public DateTime StartDate { get; set; }
	public Regularity Regularity { get; set; }
}
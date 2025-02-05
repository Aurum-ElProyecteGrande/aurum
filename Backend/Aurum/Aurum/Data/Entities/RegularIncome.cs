using Aurum.Models.RegularityEnum;

namespace Aurum.Data.Entities;

public class RegularIncome
{
	public int RegularIncomeId { get; set; }
	public int AccountId { get; set; }
	public int IncomeCategoryId { get; set; }
	public string Label  { get; set; }
	public decimal Amount { get; set; }
	public DateTime StartDate { get; set; }
	public Regularity Regularity { get; set; }
}
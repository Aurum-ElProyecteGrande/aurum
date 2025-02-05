namespace Aurum.Data.Entities;

public class IncomeCategory
{
	public int IncomeCategoryId { get; set; }
	public string Name { get; set; }
	
	public ICollection<Income> Incomes { get; set; } = [];
	public ICollection<RegularIncome> RegularIncomes { get; set; } = [];
}
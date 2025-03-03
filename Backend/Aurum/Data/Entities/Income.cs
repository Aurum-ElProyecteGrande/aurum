namespace Aurum.Data.Entities;

public class Income
{
	public int IncomeId { get; set; }
	public int AccountId { get; set; }
	public Account Account { get; set; }
	public int IncomeCategoryId { get; set; }
    public IncomeCategory IncomeCategory { get; set; }
    public string Label  { get; set; }
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
}
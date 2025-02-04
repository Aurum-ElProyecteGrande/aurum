using System.Transactions;
using Aurum.Models.ExpenseDto;

namespace Aurum.Data.Entities;

public class Account
{
	public int AccountId { get; set; }
	public int UserId { get; set; }
	public string DisplayName { get; set; }
	public decimal Amount { get; set; }
	public int CurrencyId { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; 
	
	public ICollection<Income> Incomes { get; set; } = [];
	public ICollection<Expense> Expenses { get; set; } = [];
}
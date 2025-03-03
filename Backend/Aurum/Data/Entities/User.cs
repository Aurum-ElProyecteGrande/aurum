using System.Transactions;

namespace Aurum.Data.Entities;

public class User
{
	public string UserId { get; set; }
	public string Username { get; set; }
	public string DisplayName { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

	public ICollection<Account> Accounts { get; set; } = [];
	public ICollection<ExpenseSubCategory> ExpenseSubCategories { get; set; } = [];
}
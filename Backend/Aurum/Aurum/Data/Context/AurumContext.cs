using Aurum.Data.Entities;
using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Data.Context;

public class AurumContext(DbContextOptions<AurumContext> options):DbContext(options)
{
	public DbSet<Account> Accounts { get; set; }
	public DbSet<Currency> Currencies { get; set; }
	public DbSet<Expense> Expenses { get; set; }
	public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
	public DbSet<ExpenseSubCategory> ExpenseSubCategories { get; set; }
	public DbSet<Income> Incomes { get; set; }
	public DbSet<IncomeCategory> IncomeCategories { get; set; }
	public DbSet<RegularExpense> RegularExpenses { get; set; }
	public DbSet<RegularIncome> RegularIncomes { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<BasicLayout> BasicLayouts{ get; set; }
	public DbSet<ScienticLayout> ScienticLayouts{ get; set; }
	public DbSet<DetailedLayout> DetailedLayouts { get; set; }
}
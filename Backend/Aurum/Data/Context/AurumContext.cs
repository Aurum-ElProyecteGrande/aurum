using Aurum.Data.Entities;
using Aurum.Data.Seeders.DataGenerators;
using Aurum.Data.Seeders.DataReaders;
using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.CurrencyRepository;
using Aurum.Services.CurrencyServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Data.Context;

public class AurumContext(DbContextOptions<AurumContext> options) : IdentityDbContext<IdentityUser, IdentityRole, string>(options)
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
    public DbSet<BasicLayout> BasicLayouts { get; set; }
    public DbSet<ScienticLayout> ScienticLayouts { get; set; }
    public DbSet<DetailedLayout> DetailedLayouts { get; set; }

}
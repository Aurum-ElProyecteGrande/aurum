using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Data.Seeders.DataGenerators;
using Aurum.Data.Seeders.DataReaders;
using Microsoft.AspNetCore.Identity;

namespace Aurum.Data.Seeders
{
    public class DataSeeder
    {
        private const int minimumExistingCategories = 5;

        public readonly AurumContext _context;
        public readonly UserManager<IdentityUser> _userManager;
        public readonly IConfiguration _config;
        public DataSeeder(AurumContext context, UserManager<IdentityUser> userManager, IConfiguration config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        public async Task SeedAsync()
        {
            await SeedCurrencies();
            await SeedUsers();
            await SeedAccounts();
            await SeedIncomeCategories();
            await SeedIncomes();
            await SeedExpenseCategories();
            await SeedExpenseSubCategories();
            await SeedExpenses();

        }

        private async Task SeedCurrencies()
        {
            if (!_context.Currencies.Any())
            {
                CsvDataReader<Currency> currencyReader = new CurrencyReader("currencies.csv", _config);
                var currencies = currencyReader.Read();
                await _context.Currencies.AddRangeAsync(currencies);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUsers()
        {
            CsvDataReader<IdentityUser> userReader = new UserReader("users.csv", _userManager, _config);
            UserGenerator userGenerator = new UserGenerator(_userManager, userReader.Read().ToList());
            await userGenerator.GenerateUsers();
            await userGenerator.GenerateAdmin();
        }
        private async Task SeedAccounts()
        {
            AccountGenerator accountGenerator = new(_context);

            foreach (var user in _context.Users)
            {
                var accounts = await accountGenerator.GenerateAccounts(user.Id);

                if (accounts is not null)
                {
                    await _context.Accounts.AddRangeAsync(accounts);
                    await _context.SaveChangesAsync();
                }
            }
        }

        private async Task SeedIncomeCategories()
        {
            if (_context.IncomeCategories.Count() < minimumExistingCategories)
            {
                CsvDataReader<IncomeCategory> incomeCategoryReader = new IncomeCategoryReader("income-categories.csv", _config);
                var incomeCategories = incomeCategoryReader.Read();
                await _context.IncomeCategories.AddRangeAsync(incomeCategories);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedIncomes()
        {
            CsvDataReader<IncomeCategory> incomeCategoryReader = new IncomeCategoryReader("income-categories.csv", _config);
            CsvDataReader<Dictionary<string, List<string>>> incomeReader = new IncomeReader("incomes.csv", incomeCategoryReader, _config);
            var incomeByCategoriesList = incomeReader.Read();
            var incomeByCaregories = incomeByCategoriesList.FirstOrDefault();
            IncomeGenerator incomeGenerator = new(_context, incomeByCaregories);

            foreach (var acc in _context.Accounts)
            {
                var incomes = (await incomeGenerator.GenerateIncomes(acc.AccountId));
                if (incomes is not null)
                {
                    await _context.Incomes.AddRangeAsync(incomes);
                    await _context.SaveChangesAsync();
                }
            }

        }

        private async Task SeedExpenseCategories()
        {
            if (_context.ExpenseCategories.Count() < minimumExistingCategories)
            {
                CsvDataReader<ExpenseCategory> expenseCategoryReader = new ExpenseCategoryReader("expense-categories.csv", _config);
                var expenseCategories = expenseCategoryReader.Read();
                await _context.ExpenseCategories.AddRangeAsync(expenseCategories);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedExpenseSubCategories()
        {
            if (!_context.ExpenseSubCategories.Any())
            {
                CsvDataReader<ExpenseCategory> expenseCategoryReader = new ExpenseCategoryReader("expense-categories.csv", _config);
                CsvDataReader<Dictionary<string, List<string>>> expenseSubCategoryReader = new ExpenseSubCategoryReader("expense-sub-categories.csv", expenseCategoryReader, _config);
                var expenseSubCategoriesByCategory = expenseSubCategoryReader.Read();
                ExpenseSubCategoryGenerator expenseSubCategoryGenerator = new(_context, expenseSubCategoriesByCategory.FirstOrDefault());
                var expenseSubCategories = expenseSubCategoryGenerator.GenerateExpenseSubCategories();
                await _context.ExpenseSubCategories.AddRangeAsync(expenseSubCategories);
                await _context.SaveChangesAsync();
            }

        }

        private async Task SeedExpenses()
        {
            CsvDataReader<ExpenseCategory> expenseCategoryReader = new ExpenseCategoryReader("expense-categories.csv", _config);
            CsvDataReader<Dictionary<string, List<string>>> expenseSubCategoryReader = new ExpenseSubCategoryReader("expense-sub-categories.csv", expenseCategoryReader, _config);
            CsvDataReader<Dictionary<string, Dictionary<string, List<string>>>> expenseReader = new ExpenseReader("expenses.csv", expenseSubCategoryReader, _config);

            var expensesByCatBySubList = expenseReader.Read();
            var expensesByCatBySub = expensesByCatBySubList.FirstOrDefault();

            ExpenseGenerator expenseGenerator = new(_context, expensesByCatBySub);

            foreach (var acc in _context.Accounts)
            {
                var expenses = (await expenseGenerator.GenerateExpenses(acc.AccountId));
                if (expenses is not null)
                {
                    await _context.Expenses.AddRangeAsync(expenses);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}

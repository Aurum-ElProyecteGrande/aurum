using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Data.Seeders.DataGenerators;
using Aurum.Data.Seeders.DataReaders;
using Microsoft.AspNetCore.Identity;

namespace Aurum.Data.Seeders
{
    public class DataSeeder
    {
        private const int minimumExistingIncomeCategories = 5;

        public readonly AurumContext _context;
        public readonly UserManager<IdentityUser> _userManager;

        public DataSeeder(AurumContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            await SeedCurrencies();
            await SeedUsers();
            await SeedAccounts();
            await SeedIncomeCategories();
            await SeedIncomes();
        }

        private async Task SeedCurrencies()
        {
            if (!_context.Currencies.Any())
            {
                CsvDataReader<Currency> currencyReader = new CurrencyReader("currencies.csv");
                var currencies = currencyReader.Read();
                await _context.Currencies.AddRangeAsync(currencies);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedUsers()
        {
            CsvDataReader<IdentityUser> userReader = new UserReader("users.csv", _userManager);
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
            if (_context.IncomeCategories.Count() < minimumExistingIncomeCategories)
            {
                CsvDataReader<IncomeCategory> incomeCategoryReader = new IncomeCategoryReader("income-categories.csv");
                var incomeCategories = incomeCategoryReader.Read();
                await _context.IncomeCategories.AddRangeAsync(incomeCategories);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SeedIncomes()
        {
            CsvDataReader<Dictionary<string, List<string>>> incomeReader = new IncomeReader("incomes.csv");
            IncomeGenerator incomeGenerator = new(_context);
            var incomeByCategoriesList = incomeReader.Read();
            var incomeByCaregories = incomeByCategoriesList.FirstOrDefault();

            foreach (var acc in _context.Accounts)
            {
                var incomes = (await incomeGenerator.GenerateIncomes(acc.AccountId, incomeByCaregories));
                if (incomes is not null)
                {
                    await _context.Incomes.AddRangeAsync(incomes);
                    await _context.SaveChangesAsync();
                }
            }

        }
    }
}

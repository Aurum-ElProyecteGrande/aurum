using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Data.Utils;

namespace Aurum.Data.Seeders.DataGenerators
{
    public class ExpenseGenerator
    {
        private const int enoughExpenses = 25;
        private AurumContext _context;
        private Dictionary<string, Dictionary<string, List<string>>> _expensesByCatBySub;

        public ExpenseGenerator(AurumContext context, Dictionary<string, Dictionary<string, List<string>>> expensesByCatBySub)
        {
            _context = context;
            _expensesByCatBySub = expensesByCatBySub;
        }

        public async Task<List<Expense>> GenerateExpenses(int accId)
        {
            List<Expense> expenses = new();
            var actualCategories = GetActualCategories(_expensesByCatBySub);

            DateTime today = DateTime.Now;

            for (int i = 0; i < 30; i++)
            {
                if (HasEnoughExpensesForLast30Days(today, accId, expenses)) break;

                DateTime day = today.AddDays(-i);
                if (HasExpenseOnDate(day, accId)) continue;

                var randomCatName = Utilities.GetRandomElement(actualCategories.Keys.ToList());
                var randomSubCatName = Utilities.GetRandomElement(actualCategories[randomCatName].Keys.ToList());
                var catId = _context.ExpenseCategories.FirstOrDefault(c => c.Name == randomCatName).ExpenseCategoryId;
                var subCatId = _context.ExpenseSubCategories.FirstOrDefault(c => c.Name == randomSubCatName).ExpenseSubCategoryId;
                var label = Utilities.GetRandomElement(actualCategories[randomCatName][randomSubCatName]);
                var currencyId = _context.Accounts.FirstOrDefault(a => a.AccountId == accId).CurrencyId;
                var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyId == currencyId).CurrencyCode;

                expenses.Add(new Expense
                {
                    AccountId = accId,
                    ExpenseCategoryId = catId,
                    ExpenseSubCategoryId = subCatId,
                    Label = label,
                    Amount = currency == "HUF" ? Utilities.GetRandomTens(500, 50000) : Utilities.GetRandom(1, 500),
                    Date = day
                });
            }

            return expenses;
        }

        private bool HasExpenseOnDate(DateTime day, int accId)
        {

            if (_context.Expenses.Any(e => e.AccountId == accId && e.Date.Date == day.Date)) return true;
            return false;
        }

        private Dictionary<string, Dictionary<string, List<string>>> GetActualCategories(Dictionary<string, Dictionary<string, List<string>>> expensesByCatBySub)
        {
            Dictionary<string, Dictionary<string, List<string>>> actualCategories = new();

            foreach (var cat in _context.ExpenseCategories)
            {
                if (expensesByCatBySub.ContainsKey(cat.Name))
                {
                    actualCategories.Add(cat.Name, new Dictionary<string, List<string>>());

                    foreach (var subCat in _context.ExpenseSubCategories)
                    {
                        if (expensesByCatBySub[cat.Name].ContainsKey(subCat.Name))
                        {
                            actualCategories[cat.Name].Add(subCat.Name, expensesByCatBySub[cat.Name][subCat.Name]);
                        }
                    }
                }
            }

            return actualCategories;
        }

        private bool HasEnoughExpensesForLast30Days(DateTime today, int accId, List<Expense> expenses)
        {
            DateTime firstDay = today.AddDays(-30);

            return _context.Expenses
                .Where(e => e.AccountId == accId && e.Date > firstDay && e.Date < today)
                .Count() + expenses.Count() >= enoughExpenses;

        }
    }
}

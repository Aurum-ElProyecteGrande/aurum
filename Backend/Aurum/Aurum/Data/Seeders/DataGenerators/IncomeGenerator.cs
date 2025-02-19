using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Data.Utils;

namespace Aurum.Data.Seeders.DataGenerators
{
    public class IncomeGenerator
    {
        private const int enoughIncomes = 10;
        private AurumContext _context;
        private Dictionary<string, List<string>> _incomeLabelsByCat;

        public IncomeGenerator(AurumContext context, Dictionary<string, List<string>> incomeLabelsByCat)
        {
            _context = context;
            _incomeLabelsByCat = incomeLabelsByCat;
        }

        public async Task<List<Income>> GenerateIncomes(int accId)
        {
            List<Income> incomes = new();
            var actualCategories = GetActualCategories(_incomeLabelsByCat);

            DateTime today = DateTime.Now;


            for (int i = 0; i < 30; i++)
            {
                if (HasEnoughIncomeForLast30Days(today, accId, incomes)) break;

                DateTime day = today.AddDays(-i);
                if (HasIncomeOnDate(day, accId)) continue;
                if (!Utilities.IsIncomeToday()) continue;

                var randomCatName = Utilities.GetRandomElement(actualCategories.Keys.ToList());
                var catId = _context.IncomeCategories.FirstOrDefault(c => c.Name == randomCatName).IncomeCategoryId;
                var label = Utilities.GetRandomElement(actualCategories[randomCatName]);
                var currencyId = _context.Accounts.FirstOrDefault(a => a.AccountId == accId).CurrencyId;
                var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyId == currencyId).CurrencyCode;

                incomes.Add(new Income
                {
                    AccountId = accId,
                    IncomeCategoryId = catId,
                    Label = label,
                    Amount = currency == "HUF" ? Utilities.GetRandomHundreds(1000, 100000) : Utilities.GetRandom(1, 1000),
                    Date = day
                });
            }

            return incomes;
        }

        private bool HasIncomeOnDate(DateTime day, int accId)
        {

            if (_context.Incomes.Any(i => i.AccountId == accId && i.Date.Date == day.Date)) return true;
            return false;
        }

        private Dictionary<string, List<string>> GetActualCategories(Dictionary<string, List<string>> incomeLabelsByCat)
        {
            Dictionary<string, List<string>> actualCategories = new();

            foreach (var cat in _context.IncomeCategories)
            {
                if (incomeLabelsByCat.ContainsKey(cat.Name))
                {
                    actualCategories.Add(cat.Name, incomeLabelsByCat[cat.Name]);
                }
            }

            return actualCategories;
        }

        private bool HasEnoughIncomeForLast30Days(DateTime today, int accId, List<Income> incomes)
        {
            DateTime firstDay = today.AddDays(-30);

            return _context.Incomes
                .Where(i => i.AccountId == accId && i.Date > firstDay && i.Date < today)
                .Count() + incomes.Count() >= enoughIncomes;

        }
    }
}

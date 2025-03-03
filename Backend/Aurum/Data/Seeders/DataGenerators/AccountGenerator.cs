using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Data.Seeders.DataReaders;
using Aurum.Data.Utils;
using System.Text;

namespace Aurum.Data.Seeders.DataGenerators
{
    public class AccountGenerator(AurumContext _context)
    {
        private readonly string[] _accountNames = { "General", "Savings", "Debit" };

        public async Task<List<Account>>? GenerateAccounts(string userId)
        {
            if (HasAccounts(userId)) return null;

            List<Account> accounts = new();

            foreach (string accountName in _accountNames)
            {
                var currencyId = GetCurrenyId();
                var currency = _context.Currencies.FirstOrDefault(c => c.CurrencyId == currencyId).CurrencyCode;

                accounts.Add(new Account
                {
                    UserId = userId,
                    DisplayName = accountName,
                    Amount = currency == "HUF" ? Utilities.GetRandomTenTousands(50000, 300000) : Utilities.GetRandomThousands(1000, 10000),
                    CurrencyId = currencyId
                });
            }

            return accounts;

        }

        private int GetCurrenyId()
        {
            var currencyIds = _context.Currencies.Select(c => c.CurrencyId).ToList();
            return Utilities.GetRandomElement(currencyIds);
        }

        private bool HasAccounts(string userId)
        {
            if (_context.Accounts.Any(a => a.UserId == userId)) return true;
            return false;
        }
    }
}



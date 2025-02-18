using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace Aurum.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private IAccountRepo _accountRepo;

        public AccountService(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }
        public async Task<decimal> GetInitialAmount(int accountId)
        {
            if (accountId == 0) throw new ArgumentNullException($"No account found with id {accountId}");

            var account = await Get(accountId);

            return account.Amount;
        }

        public async Task<Account> Get(int accountId)
        {
            if (accountId == 0) throw new ArgumentNullException($"No account found with id {accountId}");
            var account = await _accountRepo.Get(accountId);
            return account;
        }

        public async Task<List<Account>> GetAll(string userId)
        {
            if (String.IsNullOrEmpty(userId)) throw new ArgumentNullException($"No accounts found for userid {userId}");

            var accounts = await _accountRepo.GetAll(userId);
            return accounts;
        }
        public async Task<int> Create(ModifyAccountDto account)
        {
            var accountId = await _accountRepo.Create(ConvertModifDtoToAccount(account));

            if (accountId == 0) throw new InvalidOperationException("Failed to create account. Invalid input.");
            return accountId;
        }
        public async Task<int> Update(Account account)
        {
            var accountId = await _accountRepo.Update(account);

            if (accountId == 0) throw new InvalidOperationException("Failed to update account. Invalid input.");

            return accountId;
        }
        public async Task<bool> Delete(int accountId)
        {
            var isDeleted = await _accountRepo.Delete(accountId);

            if (!isDeleted)
                throw new InvalidOperationException($"Failed to delete account with ID {accountId}.");

            return isDeleted;
        }

        private Account ConvertModifDtoToAccount(ModifyAccountDto accDto) => new Account()
        {
            UserId = accDto.UserId,
            DisplayName = accDto.DisplayName,
            Amount = accDto.Amount,
            CurrencyId = accDto.CurrencyId,
        };
    }

}
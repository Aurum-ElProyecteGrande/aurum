using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;
using Aurum.Services.CurrencyServices;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace Aurum.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private IAccountRepo _accountRepo;
        private ICurrencyService _currencyService;

        public AccountService(IAccountRepo accountRepo, ICurrencyService currencyService)
        {
            _accountRepo = accountRepo;
            _currencyService = currencyService;
        }
        public async Task<decimal> GetInitialAmount(int accountId)
        {
            if (accountId == 0) throw new ArgumentNullException($"No account found with id {accountId}");

            var account = await Get(accountId);

            return account.Amount;
        }

        public async Task<AccountDto> Get(int accountId)
        {
            if (accountId == 0) throw new ArgumentNullException($"No account found with id {accountId}");
            var account = await _accountRepo.Get(accountId);
            var acccountDto = await ConvertAccountToDto(account);
            return acccountDto;
        }

        public async Task<List<AccountDto>> GetAll(string userId)
        {
            if (String.IsNullOrEmpty(userId)) throw new ArgumentNullException($"No accounts found for userid {userId}");

            var accounts = await _accountRepo.GetAll(userId);

            List<AccountDto> accDtos = new();

            foreach (var account in accounts)
            {
                accDtos.Add(await ConvertAccountToDto(account));
            }

            return accDtos;
        }
        public async Task<int> Create(ModifyAccountDto account)
        {
            var accountId = await _accountRepo.Create(ConvertModifDtoToAccount(account));

            if (accountId == 0) throw new InvalidOperationException("Failed to create account. Invalid input.");
            return accountId;
        }
        public async Task<int> Update(ModifyAccountDto account, int accountId)
        {
            var accToUpdate = await _accountRepo.Get(accountId);

            accToUpdate.UserId = account.UserId;
            accToUpdate.DisplayName = account.DisplayName;
            accToUpdate.CurrencyId = account.CurrencyId;
            accToUpdate.Amount = account.Amount;

            var updatedAccountId = await _accountRepo.Update(accToUpdate);

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

        private async Task<AccountDto> ConvertAccountToDto(Account acc)
        {
            var currency = await _currencyService.Get(acc.CurrencyId);
            AccountDto accDto = new(acc.AccountId, acc.UserId, acc.DisplayName, acc.Amount, currency);
            return accDto;
        }
    }

}
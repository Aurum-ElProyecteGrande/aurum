using Aurum.Data.Entities;
using Aurum.Models.AccountDto;

namespace Aurum.Services.AccountService
{
    public interface IAccountService
    {
        Task<decimal> GetInitialAmount(int accountId);
        Task<List<Account>> GetAll(string userId);
        Task<int> Create(ModifyAccountDto account);
        Task<int> Update(ModifyAccountDto account, int accountId);
        Task<bool> Delete(int accountId);
    }
}

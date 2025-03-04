using Aurum.Data.Entities;
using Aurum.Models.AccountDto;

namespace Aurum.Services.AccountService
{
    public interface IAccountService
    {
        Task<decimal> GetInitialAmount(int accountId);
        Task<AccountDto> Get(int accountId);
        Task<List<AccountDto>> GetAll(string userId);
        Task<int> Create(ModifyAccountDto account, string userId);
        Task<int> Update(ModifyAccountDto account, int accountId);
        Task<bool> Delete(int accountId);
        Task<bool> ValidCheck(string userId, int accountId);
    }
}

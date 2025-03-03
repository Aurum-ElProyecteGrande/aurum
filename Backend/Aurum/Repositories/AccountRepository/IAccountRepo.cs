using Aurum.Data.Entities;
using Aurum.Models.AccountDto;

namespace Aurum.Repositories.AccountRepository
{
    public interface IAccountRepo
    {
        Task<Account> Get(int accountId);
        Task<List<Account>> GetAll(string userId);
        Task<int> Create(Account account);
        Task<int> Update(Account account);
        Task<bool> Delete(int accountId);
    }
}
using Aurum.Data.Entities;
using Aurum.Models.AccountDto;

namespace Aurum.Repositories.AccountRepository
{
    public interface IAccountRepo
    {
        Task<List<Account>> GetAll(int accountId); 
        Task<int> Create(Account account); 
        Task<int> Update(int accountId, ModifyAccountDto account); 
        Task<bool> Delete(int accountId); 
    }
}
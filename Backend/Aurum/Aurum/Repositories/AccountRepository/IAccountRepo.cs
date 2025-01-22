using Aurum.Models.AccountDto;

namespace Aurum.Repositories.AccountRepository
{
    public interface IAccountRepo
    {
        Task<List<AccountDto>> GetAll(int accountId); 
        Task<int> Create(ModifyAccountDto account); 
        Task<int> Update(int accountId, ModifyAccountDto account); 
        Task<bool> Delete(int accountId); 
    }
}
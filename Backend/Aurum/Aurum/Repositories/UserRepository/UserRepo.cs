using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;

namespace Aurum.Repositories.UserRepository;

public class UserRepo : IAccountRepo
{
    public Task<List<AccountDto>> GetAll(int accountId)
    {
        throw new NotImplementedException();
    }
    public Task<int> Create(ModifyAccountDto account)
    {
        throw new NotImplementedException();
    }
    public Task<int> Update(int accountId, ModifyAccountDto account)
    {
        throw new NotImplementedException();
    }
    public Task<bool> Delete(int accountId)
    {
        throw new NotImplementedException();
    }
}
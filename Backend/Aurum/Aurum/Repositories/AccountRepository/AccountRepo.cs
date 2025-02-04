using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;

namespace Aurum.Repositories.AccountRepo
{
    public class AccountRepo : IAccountRepo
    {
        AurumContext _dbContext;

        public AccountRepo(AurumContext aurumContext)
        {
            _dbContext = aurumContext;
        }
        public async Task<Account> Get(int accountId) => _dbContext.Accounts
            .FirstOrDefault(a => a.AccountId == accountId);

        public async Task<List<Account>> GetAll(int userId) => _dbContext.Accounts
            .Where(a => a.UserId == userId)
            .ToList();
        public async Task<int> Create(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
            return account.AccountId;
        }
        public async Task<int> Update(Account account)
        {
            _dbContext.Update(account);
            await _dbContext.SaveChangesAsync();
            return account.AccountId;
        }
        public async Task<bool> Delete(int accountId)
        {
            var accToDelete = _dbContext.Accounts.FirstOrDefault(a => a.AccountId == accountId);

            if (accToDelete is not null)
            {
                _dbContext.Remove(accToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }
    }
}
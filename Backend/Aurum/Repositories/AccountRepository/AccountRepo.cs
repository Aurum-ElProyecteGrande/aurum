using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Repositories.AccountRepo
{
    public class AccountRepo : IAccountRepo
    {
        AurumContext _dbContext;

        public AccountRepo(AurumContext aurumContext)
        {
            _dbContext = aurumContext;
        }
        public async Task<Account> Get(int accountId) => await _dbContext.Accounts
            .Include(a => a.Currency)
            .FirstOrDefaultAsync(a => a.AccountId == accountId);

        public async Task<List<Account>> GetAll(string userId) => await _dbContext.Accounts
            .Where(a => a.UserId == userId)
            .Include(a => a.Currency)
            .ToListAsync();
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

        public async Task<bool> Exists(string userId, int accountId) => await _dbContext.Accounts
            .AnyAsync(a => a.UserId == userId && a.AccountId == accountId);
    }
}
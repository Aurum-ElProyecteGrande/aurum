using Aurum.Models.IncomeDTOs;
using Aurum.Data.Entities;
using Aurum.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Repositories.IncomeRepository.IncomeRepository
{
    public class IncomeRepo : IIncomeRepo
    {
        private AurumContext _dbContext;

        public IncomeRepo(AurumContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Income>> GetAll(int accountId) => _dbContext.Incomes
            .Where(i => i.AccountId == accountId)
            .Include(i => i.IncomeCategory)
            .Include(i => i.Account)
                .ThenInclude(a => a.Currency)
            .ToList();

        public async Task<List<Income>> GetAll(int accountId, DateTime endDate) => _dbContext.Incomes
            .Where(i => i.AccountId == accountId)
            .Where(i => i.Date < endDate)
            .Include(i => i.IncomeCategory)
            .Include(i => i.Account)
                .ThenInclude(a => a.Currency)
            .ToList();

        public async Task<List<Income>> GetAll(int accountId, DateTime startDate, DateTime endDate) => _dbContext.Incomes
            .Where(i => i.AccountId == accountId)
            .Where(i => i.Date >= startDate)
            .Where(i => i.Date <= endDate)
            .Include(i => i.IncomeCategory)
            .Include(i => i.Account)
                .ThenInclude(a => a.Currency)
            .ToList();

        public async Task<int> Create(Income income)
        {
            await _dbContext.AddAsync(income);
            await _dbContext.SaveChangesAsync();
            return income.IncomeId;
        }

        public async Task<bool> Delete(int incomeId)
        {
            var incomeToDelete = _dbContext.Incomes.FirstOrDefault(i => i.IncomeId == incomeId);
            if (incomeToDelete is not null)
            {
                _dbContext.Remove(incomeToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

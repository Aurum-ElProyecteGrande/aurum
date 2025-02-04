using Aurum.Models.IncomeDTOs;
using Aurum.Data.Entities;
using Aurum.Data.Context;

namespace Aurum.Repositories.IncomeRepository.RegularIncomeRepository
{
    public class RegularIncomeRepo : IRegularIncomeRepo
    {
        private AurumContext _dbContext;

        public RegularIncomeRepo(AurumContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<RegularIncome>> GetAllRegular(int accountId) => _dbContext.RegularIncomes
            .Where(ri => ri.AccountId == accountId)
            .ToList();
        public async Task<int> CreateRegular(RegularIncome income)
        {
            await _dbContext.RegularIncomes.AddAsync(income);
            await _dbContext.SaveChangesAsync();
            return income.RegularIncomeId;
        }
        public async Task<int> UpdateRegular(RegularIncome regularIncome)
        {
            _dbContext.RegularIncomes.Update(regularIncome);
            await _dbContext.SaveChangesAsync();
            return regularIncome.RegularIncomeId;
        }
        public async Task<bool> DeleteRegular(int regularId)
        {
            var regularIncomeToDelete = _dbContext.RegularIncomes.FirstOrDefault(i => i.RegularIncomeId == regularId);
            if (regularIncomeToDelete is not null)
            {
                _dbContext.Remove(regularIncomeToDelete);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

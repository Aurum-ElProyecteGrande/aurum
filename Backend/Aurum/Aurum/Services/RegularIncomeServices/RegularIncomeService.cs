using Aurum.Data.Entities;
using Aurum.Repositories.IncomeRepository.RegularIncomeRepository;

namespace Aurum.Services.RegularIncomeServices
{
    public class RegularIncomeService : IRegularIncomeService
    {
        IRegularIncomeRepo _regularIncomeRepo;
        public RegularIncomeService(IRegularIncomeRepo regularIncomeRepo)
        {
            _regularIncomeRepo = regularIncomeRepo;
        }
        public async Task<List<RegularIncome>> GetAllRegular(int accountId)
        {
            return await _regularIncomeRepo.GetAllRegular(accountId);
        }
        public async Task<int> CreateRegular(RegularIncome income)
        {
            return await _regularIncomeRepo.CreateRegular(income);
        }
        public async Task<int> UpdateRegular(int regularId, RegularIncome regularIncome)
        {
            return await _regularIncomeRepo.UpdateRegular(regularId, regularIncome);
        }
        public async Task<bool> DeleteRegular(int regularId)
        {
            return await _regularIncomeRepo.DeleteRegular(regularId);
        }
    }
}

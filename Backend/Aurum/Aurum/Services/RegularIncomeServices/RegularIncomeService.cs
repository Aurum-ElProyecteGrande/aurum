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
            var regularIncomeId = await _regularIncomeRepo.CreateRegular(income);

            if (regularIncomeId == 0) throw new InvalidOperationException("Invalid regular income input");

            return regularIncomeId;
        }
        public async Task<int> UpdateRegular(RegularIncome regularIncome)
        {
            var regularIncomeId = await _regularIncomeRepo.UpdateRegular(regularIncome);

            if (regularIncomeId == 0) throw new InvalidOperationException("Invalid regular income input");

            return regularIncomeId;
        }
        public async Task<bool> DeleteRegular(int regularId)
        {
            var isDeleted = await _regularIncomeRepo.DeleteRegular(regularId);

            if (!isDeleted) throw new InvalidOperationException($"Could not delete income with id {regularId}");

            return isDeleted;
        }
    }
}

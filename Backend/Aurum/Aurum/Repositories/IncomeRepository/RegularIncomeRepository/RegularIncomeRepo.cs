using Aurum.Models.IncomeDTOs;
using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.RegularIncomeRepository
{
    public class RegularIncomeRepo : IRegularIncomeRepo
    {
        public async Task<List<RegularIncome>> GetAllRegular(int accountId)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateRegular(RegularIncome income)
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateRegular(int regularId, RegularIncome regularIncome)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteRegular(int regularId)
        {
            throw new NotImplementedException();
        }
    }
}

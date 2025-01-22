using Aurum.Models.IncomeDTOs;

namespace Aurum.Repositories.Income.RegularIncome
{
    public class RegularIncomeRepo : IRegularIncomeRepo
    {
        public async Task<List<RegularIncomeDto>> GetAllRegular(int accountId)
        {
            throw new NotImplementedException();
        }
        public async Task<int> CreateRegular(ModifyRegularIncomeDto income)
        {
            throw new NotImplementedException();
        }
        public async Task<int> UpdateRegular(int regularId, ModifyRegularIncomeDto regularIncome)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> DeleteRegular(int regularId)
        {
            throw new NotImplementedException();
        }
    }
}

using Aurum.Models.IncomeDTOs;

namespace Aurum.Repositories.Income.Income
{
    public class IncomeRepo : IIncomeRepo
    {
        public async Task<List<IncomeDto>> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }
        public async Task<List<IncomeDto>> GetAll(int accountId, DateTime? startDate, DateTime? endDate)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Create(ModifyIncomeDto income)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int incomeId)
        {
            throw new NotImplementedException();
        }
    }
}

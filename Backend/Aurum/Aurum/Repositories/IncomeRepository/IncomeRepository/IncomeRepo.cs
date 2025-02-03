using Aurum.Models.IncomeDTOs;
using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.IncomeRepository
{
    public class IncomeRepo : IIncomeRepo
    {
        public async Task<List<Data.Entities.Income>> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }     
        public async Task<List<Data.Entities.Income>> GetAll(int accountId, DateTime endDate)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Data.Entities.Income>> GetAll(int accountId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
        public async Task<int> Create(Data.Entities.Income income)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int incomeId)
        {
            throw new NotImplementedException();
        }
    }
}

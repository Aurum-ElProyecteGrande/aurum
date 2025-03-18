using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.IncomeRepository
{
    public interface IIncomeRepo
    {
        Task<List<Data.Entities.Income>> GetAll(int accountId);
        Task<List<Data.Entities.Income>> GetAll(int accountId, DateTime endDate);
        Task<List<Data.Entities.Income>> GetAll(int accountId, DateTime startDate, DateTime endDate);
        Task<int> Create(Data.Entities.Income income);
        Task<bool> Delete(int incomeId);
        Task<bool> CreateRange(List<Income>  incomes);
    }
}

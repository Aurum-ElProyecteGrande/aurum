namespace Aurum.Services.IncomeServices
{
    public interface IIncomeService
    {
        (DateTime, DateTime) ValidateDates(DateTime? startDate, DateTime? endDate);
        Task<decimal> GetTotalIncome(int accountId);
        Task<decimal> GetTotalIncome(int accountId, DateTime endDate);
        Task<List<Data.Entities.Income>> GetAll(int accountId);
        Task<List<Data.Entities.Income>> GetAll(int accountId, DateTime endDate);
        Task<List<Data.Entities.Income>> GetAll(int accountId, DateTime startDate, DateTime endDate);
        Task<int> Create(Data.Entities.Income income);
        Task<bool> Delete(int incomeId);
    }
}

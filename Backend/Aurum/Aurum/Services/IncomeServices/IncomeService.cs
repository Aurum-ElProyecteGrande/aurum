using Aurum.Data.Entities;
using Aurum.Repositories.IncomeRepository.IncomeRepository;

namespace Aurum.Services.IncomeServices
{
    public class IncomeService : IIncomeService
    {
        IIncomeRepo _incomeRepo;

        public IncomeService(IIncomeRepo incomeRepo)
        {
            _incomeRepo = incomeRepo;
        }

        public (DateTime, DateTime) ValidateDates(DateTime? startDate, DateTime? endDate)
        {
            var validStartDate = new DateTime();
            var validEndDate = new DateTime();

            if (startDate.HasValue) validStartDate = startDate.Value;
            if (endDate.HasValue) validEndDate = endDate.Value;

            if (!startDate.HasValue || !endDate.HasValue) throw new NullReferenceException("Missing date");

            return (validStartDate, validEndDate);
        }

        public async Task<decimal> GetTotalIncome(int accountId)
        {
            var incomes = await _incomeRepo.GetAll(accountId);
            return incomes
                .Select(i => i.Amount)
                .Sum();
        }
        public async Task<decimal> GetTotalIncome(int accountId, DateTime endDate)
        {
            var incomes = await _incomeRepo.GetAll(accountId, endDate);
            return incomes
                .Select(i => i.Amount)
                .Sum();
        }

        public async Task<List<Income>> GetAll(int accountId)
        {

            return await _incomeRepo.GetAll(accountId);

        }
        public async Task<List<Income>> GetAll(int accountId, DateTime endDate)
        {
            return await _incomeRepo.GetAll(accountId, endDate);

        }
        public async Task<List<Income>> GetAll(int accountId, DateTime startDate, DateTime endDate)
        {
            return await _incomeRepo.GetAll(accountId, startDate, endDate);

        }
        public async Task<int> Create(Income income)
        {

            var incomeId = await _incomeRepo.Create(income);

            if (incomeId == 0) throw new InvalidOperationException("Invalid income input");

            return incomeId;
        }
        public async Task<bool> Delete(int incomeId)
        {
            var isDeleted = await _incomeRepo.Delete(incomeId);

            if (!isDeleted) throw new InvalidOperationException($"Could not delete income with id {incomeId}");

            return isDeleted;
        }
    }
}

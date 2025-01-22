using Aurum.Repositories.Income.Income;

namespace Aurum.Services.Income
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
    }
}

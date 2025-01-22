namespace Aurum.Services.Income
{
    public interface IIncomeService
    {
        (DateTime, DateTime) ValidateDates(DateTime? startDate, DateTime? endDate);

        Task<decimal> GetTotalIncome(int accountId);
        Task<decimal> GetTotalIncome(int accountId, DateTime endDate);
    }
}

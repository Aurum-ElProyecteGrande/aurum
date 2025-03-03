using Aurum.Models.BalanceDTOs;

namespace Aurum.Services.BalanceService
{
    public interface IBalanceService
    {
        DateTime ValidateDate(DateTime? date);
        Task<decimal> GetBalance(int accountId);
        Task<decimal> GetBalance(int accountId, DateTime date);
        Task<List<BalanceDto>> GetBalanceForRange(int accountId, DateTime startDate, DateTime endDate);
    }
}

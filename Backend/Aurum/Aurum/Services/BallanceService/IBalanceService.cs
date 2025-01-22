namespace Aurum.Services.BallanceService
{
    public interface IBalanceService
    {
        DateTime ValidateDate(DateTime? date);

        Task<decimal> GetBalance(int accountId);
        Task<decimal> GetBalance(int accountId, DateTime date);
    }
}

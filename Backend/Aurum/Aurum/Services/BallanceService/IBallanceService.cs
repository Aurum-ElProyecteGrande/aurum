namespace Aurum.Services.BallanceService
{
    public interface IBallanceService
    {
        DateTime ValidateDate(DateTime? date);

        Task<decimal> GetBallance(int accountId);
        Task<decimal> GetBallance(int accountId, DateTime date);
    }
}

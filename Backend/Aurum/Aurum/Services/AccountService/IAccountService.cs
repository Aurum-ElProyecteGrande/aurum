namespace Aurum.Services.AccountService
{
    public interface IAccountService
    {
        Task<decimal> GetInitialAmount(int accountId);
    }
}

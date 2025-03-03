using Aurum.Data.Entities;

namespace Aurum.Services.CurrencyServices
{
    public interface ICurrencyService
    {
        Task<Currency> Get(int currencyId);
        Task<List<Currency>> GetAll();

    }
}

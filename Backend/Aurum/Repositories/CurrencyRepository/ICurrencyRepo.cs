using Aurum.Data.Entities;

namespace Aurum.Repositories.CurrencyRepository

{
    public interface ICurrencyRepo
    {
        Task<Currency> Get(int currencyId);
        Task<List<Currency>> GetAll();
    }
}

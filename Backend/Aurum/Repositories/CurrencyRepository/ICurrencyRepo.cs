using Aurum.Data.Entities;
using Aurum.Models.CurrencyDto;

namespace Aurum.Repositories.CurrencyRepository

{
    public interface ICurrencyRepo
    {
        Task<Currency> Get(int currencyId);
        Task<List<Currency>> GetAll();
    }
}

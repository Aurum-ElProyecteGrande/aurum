using Aurum.Data.Entities;

namespace Aurum.Services.CurrencyServices
{
    public interface ICurrencyService
    {
        Task<List<Currency>> GetAll();

    }
}

using Aurum.Models.CurrencyDto;

namespace Aurum.Repositories.CurrencyRepository

{
    public interface ICurrencyRepo
    {
        Task<List<CurrencyDto>> GetAll();
    }
}

using Aurum.Data.Entities;
using Aurum.Repositories.CurrencyRepository;

namespace Aurum.Services.CurrencyServices
{
    public class CurrencyService : ICurrencyService
    {
        private ICurrencyRepo _currencyRepo;

        public CurrencyService(ICurrencyRepo currencyRepo)
        {
            _currencyRepo = currencyRepo;
        }
        public async Task<List<Currency>> GetAll()
        {
            var currencyList = await _currencyRepo.GetAll();
            return currencyList;
        }
    }
}

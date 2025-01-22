using Aurum.Models.CurrencyDto;

namespace Aurum.Repositories.CurrencyRepository;

public class CurrencyRepo : ICurrencyRepo
{
    public Task<List<CurrencyDto>> GetAll()
    {
        throw new NotImplementedException();
    }
}
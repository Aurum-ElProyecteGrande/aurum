using Aurum.Data.Entities;
using Aurum.Models.CurrencyDto;

namespace Aurum.Repositories.CurrencyRepository;

public class CurrencyRepo : ICurrencyRepo
{
    public Task<List<Currency>> GetAll()
    {
        throw new NotImplementedException();
    }
}
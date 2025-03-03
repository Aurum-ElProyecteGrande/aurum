using Aurum.Data.Context;
using Aurum.Data.Entities;

namespace Aurum.Repositories.CurrencyRepository;

public class CurrencyRepo : ICurrencyRepo
{
    AurumContext _dbContext;

    public CurrencyRepo(AurumContext aurumContext)
    {
        _dbContext = aurumContext;
    }

    public async Task<Currency> Get(int currencyId) => _dbContext.Currencies
        .FirstOrDefault(c => c.CurrencyId == currencyId);


    public async Task<List<Currency>> GetAll() => _dbContext.Currencies
        .ToList();
}
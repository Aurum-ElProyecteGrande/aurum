using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.CurrencyDto;

namespace Aurum.Repositories.CurrencyRepository;

public class CurrencyRepo : ICurrencyRepo
{
    AurumContext _dbContext;

    public CurrencyRepo(AurumContext aurumContext)
    {
        _dbContext = aurumContext;
    }
    public async Task<List<Currency>> GetAll() => _dbContext.Currencies
        .ToList();
}
using Aurum.Data.Context;
using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.IncomeCategoryRepository
{
    public class IncomeCategoryRepo : IIncomeCategoryRepo
    {
        private AurumContext _dbContext;

        public IncomeCategoryRepo(AurumContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<IncomeCategory>> GetAllCategory() => _dbContext.IncomeCategories
            .ToList();

    }
}

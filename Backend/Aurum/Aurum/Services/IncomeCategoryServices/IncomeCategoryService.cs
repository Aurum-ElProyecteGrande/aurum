using Aurum.Data.Entities;
using Aurum.Repositories.IncomeRepository.IncomeCategoryRepository;

namespace Aurum.Services.IncomeCategoryServices
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private IIncomeCategoryRepo _incomeCategoryRepo;

        public IncomeCategoryService (IIncomeCategoryRepo incomeCategoryRepo)
        {
            _incomeCategoryRepo = incomeCategoryRepo;
        }
        public async Task<List<IncomeCategory>> GetAllCategory()
        {
            return await _incomeCategoryRepo.GetAllCategory();
        }

    }
}

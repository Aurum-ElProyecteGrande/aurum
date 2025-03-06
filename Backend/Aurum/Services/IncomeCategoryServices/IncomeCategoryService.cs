using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Repositories.IncomeRepository.IncomeCategoryRepository;

namespace Aurum.Services.IncomeCategoryServices
{
    public class IncomeCategoryService : IIncomeCategoryService
    {
        private IIncomeCategoryRepo _incomeCategoryRepo;

        public IncomeCategoryService(IIncomeCategoryRepo incomeCategoryRepo)
        {
            _incomeCategoryRepo = incomeCategoryRepo;
        }
        public async Task<List<CategoryDto>> GetAllCategory()
        {
            var categories = await _incomeCategoryRepo.GetAllCategory();
            return categories.Select(ConvertToDto).ToList();
        }

        private CategoryDto ConvertToDto(IncomeCategory category)
        {
            return new(category.Name, category.IncomeCategoryId);
        }
    }
}

using Aurum.Models.CategoryDtos;

namespace Aurum.Repositories.Income.IncomeCategory
{
    public interface IIncomeCategoryRepo
    {
        Task<List<CategoryDto>> GetAllCategory();

    }
}

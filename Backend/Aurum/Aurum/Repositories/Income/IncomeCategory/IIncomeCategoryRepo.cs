using Aurum.Models.CategoryDTOs;

namespace Aurum.Repositories.Income.IncomeCategory
{
    public interface IIncomeCategoryRepo
    {
        Task<List<CategoryDto>> GetAllCategory();

    }
}

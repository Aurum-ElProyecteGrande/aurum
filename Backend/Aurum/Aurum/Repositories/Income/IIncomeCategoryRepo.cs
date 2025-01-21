using Aurum.Models.CategoryDTOs;

namespace Aurum.Repositories.Income
{
    public interface IIncomeCategoryRepo
    {
        Task<List<CategoryDto>> GetAllCategory();

    }
}

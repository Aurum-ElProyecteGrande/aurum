using Aurum.Models.CategoryDTOs;

namespace Aurum.Repositories.Income
{
    public interface IIncomeCategoryRepo
    {
        List<CategoryDto> GetAllCategory();

    }
}

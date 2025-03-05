using Aurum.Models.CategoryDtos;

namespace Aurum.Services.IncomeCategoryServices
{
    public interface IIncomeCategoryService
    {
        Task<List<CategoryDto>> GetAllCategory();
    }
}

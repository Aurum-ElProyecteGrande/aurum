using Aurum.Models.CategoryDto;

namespace Aurum.Repositories.ExpenseCategoryRepository;

public interface IExpenseCategoryRepository
{
	Task<List<CategoryDto>> GetAllCategory();
	Task<List<SubCategoryDto>> GetAllSubCategory(int userId);
	Task<int?> GetSubCategoryByName(int categoryId, string subCategoryName);
	Task<int> CreateSubCategory(int categoryId, string subCategoryName);
}
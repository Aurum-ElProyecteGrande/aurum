using Aurum.Models.CategoryDto;

namespace Aurum.Repositories.ExpenseCategoryRepository;

public interface IExpenseCategoryRepository
{
	List<CategoryDto> GetAllCategory();
	List<SubCategoryDto> GetAllSubCategory(int userId);
	int? GetSubCategoryByName(int categoryId, string subCategoryName);
	int CreateSubCategory(int categoryId, string subCategoryName);
}
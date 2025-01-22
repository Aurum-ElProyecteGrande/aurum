using Aurum.Models.CategoryDtos;

namespace Aurum.Services.ExpenseCategoryService;

public interface IExpenseCategoryService
{
	Task<Dictionary<CategoryDto, List<SubCategoryDto>>> GetAllExpenseCategories(int userId);
	Task<int> AcquireSubCategoryId(int categoryId, string subCategoryName);
}
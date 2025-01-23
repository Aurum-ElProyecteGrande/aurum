using Aurum.Models.CategoryDtos;

namespace Aurum.Repositories.ExpenseCategoryRepository;

public class ExpenseCategoryRepository: IExpenseCategoryRepository
{
	public Task<List<CategoryDto>> GetAllCategory()
	{
		throw new NotImplementedException();
	}

	public Task<List<SubCategoryDto>> GetAllSubCategory(int userId)
	{
		throw new NotImplementedException();
	}

	public Task<int?> GetSubCategoryByName(int categoryId, string subCategoryName)
	{
		throw new NotImplementedException();
	}

	public Task<int> CreateSubCategory(int categoryId, string subCategoryName)
	{
		throw new NotImplementedException();
	}
}
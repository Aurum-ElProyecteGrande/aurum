using Aurum.Models.CategoryDto;
using Aurum.Repositories.ExpenseCategoryRepository;

namespace Aurum.Services.ExpenseCategoryService;

public class ExpenseCategoryService(IExpenseCategoryRepository repository): IExpenseCategoryService
{
	private IExpenseCategoryRepository _repository = repository;
	
	public async Task<Dictionary<CategoryDto, List<SubCategoryDto>>> GetAllExpenseCategories(int userId)
	{
		// Retrieve categories and subcategories
		var categories = await _repository.GetAllCategory();
		var subCategories = await _repository.GetAllSubCategory(userId);

		// Validate inputs
		if (categories == null || subCategories == null)
			throw new InvalidOperationException("Failed to retrieve expense categories or subcategories.");

		if (categories.Count == 0 || subCategories.Count == 0)
			throw new InvalidOperationException("No expense categories found.");

		//Convert categories to a dictionary for fast lookup
		var categoryDict = categories.ToDictionary(c => c.CategoryId);
		
		return subCategories.GroupBy(s => s.CategoryId)
			.ToDictionary(
				g => categoryDict[g.Key],
				g => g.ToList()
			);
	}

	public async Task<int> AcquireSubCategoryId(int categoryId, string subCategoryName)
	{
		//Try to get the SubCategoryId, or create it if not found
		var subCategoryId = await GetSubCategoryId(categoryId, subCategoryName) 
		                    ?? await CreateSubCategory(categoryId, subCategoryName);

		//Ensure SubCategoryId is valid
		return subCategoryId ?? throw new InvalidOperationException("Failed to acquire SubCategoryId.");
	}

	private async Task<List<CategoryDto>> GetAllCategories() => 
		await _repository.GetAllCategory();

	private async Task<List<SubCategoryDto>> GetAllSubCategories(int userId) => 
		await _repository.GetAllSubCategory(userId);
	
	private async Task<int?> GetSubCategoryId(int categoryId,string subCategoryName) => 
		await _repository.GetSubCategoryByName(categoryId, subCategoryName);
	
	private async Task<int?> CreateSubCategory(int categoryId,string subCategoryName) =>
		await _repository.CreateSubCategory(categoryId, subCategoryName);
}
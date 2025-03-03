using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;



public interface IExpenseCategoryRepository
{
	Task<List<ExpenseCategory>> GetAllCategory();
	Task<List<ExpenseSubCategory>> GetAllSubCategory(string userId);
	Task<int?> GetSubCategoryByName(int categoryId, string subCategoryName);
	Task<int> CreateSubCategory(int categoryId, string subCategoryName);
}
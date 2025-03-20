using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Microsoft.EntityFrameworkCore;

namespace Aurum.Repositories.ExpenseCategoryRepository;

public class ExpenseCategoryRepository(AurumContext aurumContext): IExpenseCategoryRepository
{
	private readonly AurumContext _context = aurumContext;

	public async Task<List<ExpenseCategory>> GetAllCategory() =>
		await _context.ExpenseCategories.ToListAsync();


	public async Task<List<ExpenseSubCategory>> GetAllSubCategory(string userId) =>
		await _context.ExpenseSubCategories
			.Where(s => s.IsBase || s.UserId == userId)
			.ToListAsync();

	public async Task<int?> GetSubCategoryByName(int categoryId, string subCategoryName)
	{
		var category = await _context.ExpenseCategories
			.Where(c => c.ExpenseCategoryId == categoryId && c.ExpenseSubCategories
				.Any(s => s.Name.ToLower() == subCategoryName.ToLower()))
			.FirstOrDefaultAsync();

		return category?.ExpenseCategoryId;
	}
	public async Task<int> CreateSubCategory(int categoryId, string subCategoryName)
	{
		var subCategory = new ExpenseSubCategory()
		{
			ExpenseCategoryId = categoryId,
			Name = subCategoryName
		};

		_context.ExpenseSubCategories.Add(subCategory);
		await _context.SaveChangesAsync();

		return subCategory.ExpenseSubCategoryId;
	}
}

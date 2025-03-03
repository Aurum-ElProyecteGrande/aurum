using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.RegularExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;

namespace Aurum.Services.RegularExpenseService;

public class RegularExpenseService(IRegularExpenseRepository repository,IExpenseCategoryService categoryService): IRegularExpenseService
{
	private readonly IRegularExpenseRepository _repository = repository;
	private readonly IExpenseCategoryService _categoryService = categoryService;
	
	public async Task<List<RegularExpenseDto>> GetAll(int accountId, string userId)
	{
		var rawExpenses = await _repository.GetAllRegular(accountId);
		
		if (rawExpenses.Count == 0)
			return [];
		
		var categories = await _categoryService.GetAllExpenseCategories(userId);
		
		if (categories.Count == 0)
			throw new InvalidDataException("No categories found");
		
		return CreateRegularExpenseDtoList(rawExpenses, categories);
	}

	public async Task<int> Create(int regularId, ModifyRegularExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		var expense = CreateRawRegularExpenseDto(expenseDto, regularId, subCategoryId);
		
		return await _repository.Create(expense); 
	}

	public async Task<int> Update(int regularId, ModifyRegularExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		var expense = CreateRawRegularExpenseDto(expenseDto, regularId, subCategoryId);
		
		return await _repository.Update(expense); 
	}

	public async Task<bool> Delete(int expenseId) => 
		await _repository.Delete(expenseId);

	private List<RegularExpenseDto> CreateRegularExpenseDtoList(
		List<RegularExpense> expenseDtoList,
		Dictionary<CategoryDto, List<SubCategoryDto>> categories
	)
	{
		var categoryDict = categories.ToDictionary(c => c.Key.CategoryId);
		var expenses = new List<RegularExpenseDto>();

		foreach (var expenseDto in expenseDtoList)
		{
			//For faster lookup time
			if (!categoryDict.TryGetValue(expenseDto.ExpenseCategoryId, out var categoryKvp))
				throw new KeyNotFoundException($"Category with ID {expenseDto.ExpenseSubcategoryId} not found");

			var category = categoryKvp.Key;
			var subCategories = categoryKvp.Value;
			
			var subCategory = subCategories.FirstOrDefault(s => s.SubCategoryId == expenseDto.ExpenseSubcategoryId);
			
			var expense = CreateRegularExpenseDto(expenseDto, category, subCategory);
			expenses.Add(expense);
		}
		
		return expenses;
	}

	private RegularExpense CreateRawRegularExpenseDto(
		ModifyRegularExpenseDto expense,
		int regularId,
		int? subCategoryId
	) =>
		new RegularExpense()
		{
			RegularExpenseId = regularId,
			AccountId = expense.AccountId,
			ExpenseCategoryId = expense.CategoryId,
			ExpenseSubcategoryId = subCategoryId ?? null,
			Label = expense.Label,
			Amount = expense.Amount,
			StartDate = expense.StartDate,
			Regularity = expense.Regularity
		};
	private RegularExpenseDto CreateRegularExpenseDto(
		RegularExpense expense, 
		CategoryDto category, 
		SubCategoryDto? subCategory
		) => 
		new RegularExpenseDto(
			expense.RegularExpenseId,
			expense.AccountId,
			category,
			subCategory ?? null,
			expense.Label,
			expense.Amount,
			expense.StartDate,
			expense.Regularity
		);
	
}
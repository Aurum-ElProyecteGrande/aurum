using Aurum.Models.CategoryDto;
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
	
	public async Task<List<RegularExpenseDto>> GetAll(int accountId, int userId)
	{
		var rawExpenses = await _repository.GetAllRegular(accountId);
		
		if (rawExpenses.Count == 0)
			return [];
		
		var categories = await _categoryService.GetAllExpenseCategories(userId);
		
		if (categories.Count == 0)
			throw new InvalidDataException("No categories found");
		
		return CreateRegularExpenseDtoList(rawExpenses, categories);
	}

	public async Task<int> Create(ModifyRegularExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		var expense = CreateRawRegularExpenseDto(expenseDto, subCategoryId);
		
		return await _repository.Create(expense); 
	}

	public async Task<int> Update(ModifyRegularExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		var expense = CreateRawRegularExpenseDto(expenseDto, subCategoryId);
		
		return await _repository.Update(expense); 
	}

	public async Task<bool> Delete(int expenseId) => 
		await _repository.Delete(expenseId);

	private List<RegularExpenseDto> CreateRegularExpenseDtoList(
		List<RawRegularExpenseDto> expenseDtoList,
		Dictionary<CategoryDto, List<SubCategoryDto>> categories
	)
	{
		var categoryDict = categories.ToDictionary(c => c.Key.CategoryId);
		var expenses = new List<RegularExpenseDto>();

		foreach (var expenseDto in expenseDtoList)
		{
			//For faster lookup time
			if (!categoryDict.TryGetValue(expenseDto.CategoryId, out var categoryKvp))
				throw new KeyNotFoundException($"Category with ID {expenseDto.CategoryId} not found");

			var category = categoryKvp.Key;
			var subCategories = categoryKvp.Value;
			
			var subCategory = subCategories.FirstOrDefault(s => s.SubCategoryId == expenseDto.SubcategoryId);
			
			var expense = CreateRegularExpenseDto(expenseDto, category, subCategory);
			expenses.Add(expense);
		}
		
		return expenses;
	}
	
	private RawRegularExpenseDto CreateRawRegularExpenseDto(
		ModifyRegularExpenseDto expense, 
		int? subCategoryId
		) => 
		new RawRegularExpenseDto(
			expense.RegularId,
			expense.AccountId,
			expense.CategoryId,
			subCategoryId ?? null,
			expense.Label,
			expense.Amount,
			expense.StartDate,
			expense.Regularity
		);
	private RegularExpenseDto CreateRegularExpenseDto(
		RawRegularExpenseDto expense, 
		CategoryDto category, 
		SubCategoryDto? subCategory
		) => 
		new RegularExpenseDto(
			expense.RegularId,
			expense.AccountId,
			category,
			subCategory ?? null,
			expense.Label,
			expense.Amount,
			expense.StartDate,
			expense.Regularity
		);
	
}
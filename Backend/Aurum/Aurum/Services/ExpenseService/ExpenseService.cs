using Aurum.Models.CategoryDto;
using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Services.ExpenseCategoryService;

namespace Aurum.Services.ExpenseService;

public class ExpenseService(IExpenseRepository repository, IExpenseCategoryService categoryService): IExpenseService
{
	private readonly IExpenseRepository _repository = repository;
	private readonly IExpenseCategoryService _categoryService = categoryService;
	
	public async Task<List<ExpenseDto>> GetAll(int accountId, int userId)
	{
		var rawData = await _repository.GetAll(accountId);
		var categories = await _categoryService.GetAllExpenseCategories(userId);

		if (rawData.Count == 0)
			return [];

		if (categories.Count == 0)
			throw new InvalidDataException("No categories found");
		
		var categoryDict = categories.ToDictionary(c => c.Key.CategoryId);
		
		return CreateExpenseDtoList(rawData, categoryDict);
		
	}

	public async Task<List<ExpenseDto>> GetAll(int accountId, int userId, DateTime startDate, DateTime endDate)
	{
		var rawData = await _repository.GetAll(accountId, startDate, endDate);
		var categories = await _categoryService.GetAllExpenseCategories(userId);

		if (rawData.Count == 0)
			return [];

		if (categories.Count == 0)
			throw new InvalidDataException("No categories found");
		
		var categoryDict = categories.ToDictionary(c => c.Key.CategoryId);
		
		return CreateExpenseDtoList(rawData, categoryDict);
	}

	public async Task<int> Create(ModifyExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		
		var expense = CreateRawExpenseDto(expenseDto, subCategoryId);
		
		return await _repository.Create(expense);
	}

	public async Task<bool> Delete(int expenseId) => 
		await _repository.Delete(expenseId);

	private List<ExpenseDto> CreateExpenseDtoList(
			List<RawExpenseDto> rawExpenses,
		Dictionary<int,KeyValuePair<CategoryDto, List<SubCategoryDto>>> categoryDict
			)=> 
		rawExpenses.Select(rawExpense => CreateExpenseDto(categoryDict, rawExpense))
			.ToList();
	

	private ExpenseDto CreateExpenseDto(Dictionary<int, KeyValuePair<CategoryDto, List<SubCategoryDto>>> categoryDict, RawExpenseDto rawExpense)
	{
		//For faster lookup time
		if (!categoryDict.TryGetValue(rawExpense.CategoryId, out var categoryKvp))
			throw new KeyNotFoundException($"Category with ID {rawExpense.CategoryId} not found");

		var category = categoryKvp.Key;
		var subCategories = categoryKvp.Value;
			
		var subCategory = subCategories.FirstOrDefault(s => s.SubCategoryId == rawExpense.SubCategoryId);
		
		return CreateExpenseDto(rawExpense, category, subCategory);
	}
	
	private ExpenseDto CreateExpenseDto(RawExpenseDto expense, CategoryDto category, SubCategoryDto? subCategory) => 
		new ExpenseDto(
			category,
			subCategory ?? null,
			expense.Label,
			expense.Amount,
			expense.Date
		);

	private RawExpenseDto CreateRawExpenseDto(ModifyExpenseDto expenseDto, int? subCategoryId) =>
		new RawExpenseDto(
			expenseDto.CategoryId,
			subCategoryId ?? null,
			expenseDto.Label,
			expenseDto.Amount,
			expenseDto.Date
			);
}
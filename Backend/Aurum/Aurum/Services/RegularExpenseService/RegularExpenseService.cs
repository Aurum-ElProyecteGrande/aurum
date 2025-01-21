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
		var categories = await _categoryService.GetAllExpenseCategories(userId);
		var categoryDict = categories
			.ToDictionary(c => c.Key.CategoryId);
		var rawExpenses = await _repository.GetAllRegular(accountId);
		
		return CreateRegularExpenseDtoList(rawExpenses, categoryDict);
	}

	public async Task<int> Create(int userdId, ModifyRegularExpenseDto expenseDto)
	{
		var categories = await _categoryService.GetAllExpenseCategories(userdId);
		var categoryDict = categories
			.ToDictionary(c => c.Key.CategoryId);
		var expense = CreateRegularExpenseDto(expenseDto, categoryDict);
		
		return await _repository.Create(expense); 
	}

	public async Task<int> Update(int userId, ModifyRegularExpenseDto expenseDto)
	{
		var categories = await _categoryService.GetAllExpenseCategories(userId);
		var categoryDict = categories
			.ToDictionary(c => c.Key.CategoryId);
		var expense = CreateRegularExpenseDto(expenseDto, categoryDict);
		
		return await _repository.Update(expense); 
	}

	public async Task<bool> Delete(int expenseId) => 
		await _repository.Delete(expenseId);

	private List<RegularExpenseDto> CreateRegularExpenseDtoList(
		List<RawRegularExpenseDto> expenseDtoList,
		Dictionary<int, KeyValuePair<CategoryDto, List<SubCategoryDto>>> categoryDict
		) => 
		expenseDtoList
			.Select(expenseDto => 
				CreateRegularExpenseDto(expenseDto, categoryDict))
			.ToList();
	

	private RegularExpenseDto CreateRegularExpenseDto(
		ModifyRegularExpenseDto expenseDto,
		Dictionary<int, KeyValuePair<CategoryDto, List<SubCategoryDto>>> categoryDict
		)
	{
		//For faster lookup time
		if (!categoryDict.TryGetValue(expenseDto.CategoryId, out var categoryKvp))
			throw new KeyNotFoundException($"Category with ID {expenseDto.CategoryId} not found");

		var category = categoryKvp.Key;
		var subCategories = categoryKvp.Value;
			
		var subCategory = subCategories.FirstOrDefault(s => s.Name == expenseDto.SubCategoryName);
		
		return CreateRegularExpenseDto(expenseDto, category, subCategory);
	}
	
	private RegularExpenseDto CreateRegularExpenseDto(
		RawRegularExpenseDto expenseDto,
		Dictionary<int, KeyValuePair<CategoryDto, List<SubCategoryDto>>> categoryDict
		)
	{
		//For faster lookup time
		if (!categoryDict.TryGetValue(expenseDto.CategoryId, out var categoryKvp))
			throw new KeyNotFoundException($"Category with ID {expenseDto.CategoryId} not found");

		var category = categoryKvp.Key;
		var subCategories = categoryKvp.Value;
			
		var subCategory = subCategories.FirstOrDefault(s => s.SubCategoryId == expenseDto.SubcategoryId);
		
		return CreateRegularExpenseDto(expenseDto, category, subCategory);
	}
	
	private RegularExpenseDto CreateRegularExpenseDto(
		ModifyRegularExpenseDto expense, 
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
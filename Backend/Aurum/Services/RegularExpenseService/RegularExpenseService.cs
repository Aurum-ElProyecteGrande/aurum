using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;
using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDTO;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.RegularExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;

namespace Aurum.Services.RegularExpenseService;

public class RegularExpenseService(IRegularExpenseRepository repository,IExpenseCategoryService categoryService): IRegularExpenseService
{
	private readonly IRegularExpenseRepository _repository = repository;
	private readonly IExpenseCategoryService _categoryService = categoryService;

	public async Task<List<RegularExpenseDto>> GetAllWithId(int accountId)
	{
		var rawExpenses = await _repository.GetAllRegularWithId(accountId);

		if (rawExpenses.Count == 0)
			return [];

		return rawExpenses.Select(CreateRegularExpenseDto).ToList();
	}

	public async Task<List<RegularExpense>> GetAll()
	{
		var rawExpenses = await _repository.GetAllRegular();

		if (rawExpenses.Count == 0)
			return [];

		return rawExpenses;
	}

	public async Task<int> Create(ModifyRegularExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		var expense = CreateRegularExpense(expenseDto, subCategoryId);

		return await _repository.Create(expense);
	}

	public async Task<int> Update(int regularId, ModifyRegularExpenseDto expenseDto)
	{
		var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
			await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId,expenseDto.SubCategoryName);
		var expense = CreateRegularExpense(expenseDto, subCategoryId);

		return await _repository.Update(expense);
	}

	public async Task<bool> Delete(int expenseId) =>
		await _repository.Delete(expenseId);

	private RegularExpense CreateRegularExpense(
		ModifyRegularExpenseDto expense,
		int? subCategoryId
	) =>
		new RegularExpense()
		{
			AccountId = expense.AccountId,
			ExpenseCategoryId = expense.CategoryId,
			ExpenseSubCategoryId = subCategoryId ?? null,
			Label = expense.Label,
			Amount = expense.Amount,
			StartDate = expense.StartDate,
			Regularity = expense.Regularity
		};

	private RegularExpenseDto CreateRegularExpenseDto(RegularExpense regularExpense)
	{
		var categoryDto = new CategoryDto(regularExpense.ExpenseCategory.Name, regularExpense.ExpenseCategoryId);

		SubCategoryDto subCategoryDto = null;
		if (regularExpense.ExpenseSubCategoryId != null)
		{
			subCategoryDto = new SubCategoryDto(
				regularExpense.ExpenseSubCategory.Name,
				regularExpense.ExpenseSubCategory.ExpenseSubCategoryId,
				regularExpense.ExpenseCategoryId
			);
		}

		var currency = new CurrencyDto(regularExpense.Account.Currency.Name, regularExpense.Account.Currency.CurrencyCode,
			regularExpense.Account.Currency.Symbol);

		return new RegularExpenseDto(
			regularExpense.RegularExpenseId,
			currency,
			categoryDto,
			subCategoryDto,
			regularExpense.Label,
			regularExpense.Amount,
			regularExpense.StartDate,
			regularExpense.Regularity
		);
	}

}

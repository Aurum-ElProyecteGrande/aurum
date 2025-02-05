using Aurum.Models.CategoryDtos;
using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using System.Linq;
using Aurum.Data.Entities;

namespace Aurum.Services.ExpenseService;

public class ExpenseService(IExpenseRepository repository, IExpenseCategoryService categoryService) : IExpenseService
{
    private readonly IExpenseRepository _repository = repository;
    private readonly IExpenseCategoryService _categoryService = categoryService;

    public async Task<List<ExpenseDto>> GetAll(int accountId, int userId)
    {
        var rawData = await _repository.GetAll(accountId);

        if (rawData.Count == 0)
            return [];

        var categories = await _categoryService.GetAllExpenseCategories(userId);

        if (categories.Count == 0)
            throw new InvalidDataException("No categories found");

        return CreateExpenseDtoList(rawData, categories);

    }

    public async Task<List<ExpenseDto>> GetAll(int accountId, int userId, DateTime startDate, DateTime endDate)
    {
        var rawData = await _repository.GetAll(accountId, startDate, endDate);

        if (rawData.Count == 0)
            return [];

        var categories = await _categoryService.GetAllExpenseCategories(userId);

        if (categories.Count == 0)
            throw new InvalidDataException("No categories found");

        return CreateExpenseDtoList(rawData, categories);
    }

    public async Task<int> Create(ModifyExpenseDto expenseDto)
    {
        var subCategoryId = string.IsNullOrEmpty(expenseDto.SubCategoryName) ? (int?)null :
            await _categoryService.AcquireSubCategoryId(expenseDto.CategoryId, expenseDto.SubCategoryName);

        var expense = CreateRawExpenseDto(expenseDto, subCategoryId);

        return await _repository.Create(expense);
    }

    public async Task<bool> Delete(int expenseId) =>
        await _repository.Delete(expenseId);

    private List<ExpenseDto> CreateExpenseDtoList(
        List<Expense> rawExpenses,
        Dictionary<CategoryDto, List<SubCategoryDto>> categories)
    {
        var categoryDict = categories.ToDictionary(c => c.Key.CategoryId);
        var expenses = new List<ExpenseDto>();

        foreach (var rawExpense in rawExpenses)
        {
            //For faster lookup
            if (!categoryDict.TryGetValue(rawExpense.ExpenseCategoryId, out var categoryKvp))
                throw new KeyNotFoundException($"Category with ID {rawExpense.ExpenseCategoryId} not found");

            var category = categoryKvp.Key;
            var subCategories = categoryKvp.Value;

            var subCategory = subCategories.FirstOrDefault(s => s.SubCategoryId == rawExpense.ExpenseSubCategoryId);

            var expense = CreateExpenseDto(rawExpense, category, subCategory);

            expenses.Add(expense);
        }

        return expenses;
    }

    private ExpenseDto CreateExpenseDto(Expense expense, CategoryDto category, SubCategoryDto? subCategory) =>
        new ExpenseDto(
            category,
            subCategory ?? null,
            expense.Label,
            expense.Amount,
            expense.Date
        );

    private Expense CreateRawExpenseDto(ModifyExpenseDto expenseDto, int? subCategoryId) =>
        new Expense()
        {
            ExpenseCategoryId = expenseDto.CategoryId,
            ExpenseSubCategoryId = subCategoryId ?? null,
            Label = expenseDto.Label,
            Amount = expenseDto.Amount,
            Date = expenseDto.Date
        };

    public async Task<decimal> GetTotalExpense(int accountId)
    {
        var expenses = await _repository.GetAll(accountId);
        return expenses
            .Select(e => e.Amount)
            .Sum();
    }
    public async Task<decimal> GetTotalExpense(int accountId, DateTime date)
    {
        var expenses = await _repository.GetAll(accountId, date);
        return expenses
            .Select(e => e.Amount)
            .Sum();
    }
}


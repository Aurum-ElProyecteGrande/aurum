using Aurum.Models.CategoryDtos;
using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using System.Linq;
using Aurum.Data.Entities;
using Aurum.Models.CurrencyDtos;
using Aurum.Models.ExpenseDTO;
using Aurum.Models.IncomeDTOs;
using Aurum.Services.AccountService;
using Aurum.Services.CurrencyServices;

namespace Aurum.Services.ExpenseService;

public class ExpenseService(IExpenseRepository repository, IExpenseCategoryService categoryService) : IExpenseService
{
    private readonly IExpenseRepository _repository = repository;
    private readonly IExpenseCategoryService _categoryService = categoryService;

    public async Task<List<ExpenseDto>> GetAll(int accountId)
    {
        var rawData = await _repository.GetAll(accountId);

        if (rawData.Count == 0) 
            return [];
        
        return rawData.Select(CreateExpenseDto).ToList();

    }

    public async Task<List<ExpenseDto>> GetAll(int accountId, DateTime startDate, DateTime endDate)
    {
        var rawData = await _repository.GetAll(accountId, startDate, endDate);

        if (rawData.Count == 0)
            return [];
        
        return rawData.Select(CreateExpenseDto).ToList();
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
    
    private ExpenseDto CreateExpenseDto(Expense expense)
    {
        var categoryDto = new CategoryDto(expense.ExpenseCategory.Name, expense.ExpenseCategoryId);
        
        SubCategoryDto subCategoryDto = null;
        if (expense.ExpenseSubCategoryId != null)
        {
            subCategoryDto = new SubCategoryDto(
                expense.ExpenseSubCategory.Name, 
                expense.ExpenseSubCategory.ExpenseSubCategoryId, 
                expense.ExpenseCategoryId
            );
        }

        var currency = new CurrencyDto(expense.Account.Currency.Name, expense.Account.Currency.CurrencyCode,
            expense.Account.Currency.Symbol);
        
        return new ExpenseDto(
            currency,
            categoryDto,
            subCategoryDto,
            expense.Label,
            expense.Amount,
            expense.Date
        );
    }



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


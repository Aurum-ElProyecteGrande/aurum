using Aurum.Data.Entities;
using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Services.RegularExpenseService;

public interface IRegularExpenseService
{
	Task<List<RegularExpenseDto>> GetAllWithId(int accountId);
	Task<List<RegularExpense>> GetAll();
	Task<int> Create(ModifyRegularExpenseDto expense);
	Task<int> Update(int regularId, ModifyRegularExpenseDto expense);
	Task<bool> Delete(int expenseId);
}

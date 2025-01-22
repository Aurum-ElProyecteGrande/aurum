using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Services.RegularExpenseService;

public interface IRegularExpenseService
{
	Task<List<RegularExpenseDto>> GetAll(int accountId, int userId);
	Task<int> Create(ModifyRegularExpenseDto expense);
	Task<int> Update(ModifyRegularExpenseDto expense);
	Task<bool> Delete(int expenseId);
}
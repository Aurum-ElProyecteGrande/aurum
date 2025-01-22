using Aurum.Models.ExpenseDto;
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Services.RegularExpenseService;

public interface IRegularExpenseService
{
	Task<List<RegularExpenseDto>> GetAll(int accountId, int userId);
	Task<int> Create(int regularId, ModifyRegularExpenseDto expense);
	Task<int> Update(int regularId, ModifyRegularExpenseDto expense);
	Task<bool> Delete(int expenseId);
}
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Services.RegularExpenseService;

public interface IRegularExpenseService
{
	Task<List<RegularExpenseDto>> GetAll(int accountId, int userId);
	Task<int> Create(int userId, ModifyRegularExpenseDto expense);
	Task<int> Update(int userId, ModifyRegularExpenseDto expense);
	Task<bool> Delete(int expenseId);
}
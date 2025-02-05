using Aurum.Data.Entities;
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public interface IRegularExpenseRepository
{
	Task<List<RegularExpense>> GetAllRegular(int accountId);
	Task<int> Create(RegularExpense expense);
	Task<int> Update(RegularExpense expense);
	Task<bool> Delete(int regularId);
}
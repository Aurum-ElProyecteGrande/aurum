using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public interface IRegularExpenseRepository
{
	Task<List<RawRegularExpenseDto>> GetAllRegular(int accountId);
	Task<int> Create(RegularExpenseDto expense);
	Task<int> Update(RegularExpenseDto expense);
	Task<bool> Delete(int regularId);
}
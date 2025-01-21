using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public interface IRegularExpenseRepository
{
	Task<List<RawRegularExpenseDto>> GetAllRegular(int accountId);
	Task<int> Create(RawRegularExpenseDto expense);
	Task<int> Update(RawRegularExpenseDto expense);
	Task<bool> Delete(int regularId);
}
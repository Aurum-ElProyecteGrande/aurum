using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public interface IRegularExpenseRepository
{
	Task<List<RawRegularExpenseDto>> GetAllRegular(int accountId);
	Task<int> CreateRegular(ModifyRegularExpenseDto expense);
	Task<int> UpdateRegular(int regularId, RawRegularExpenseDto expense);
	Task<bool> DeleteRegular(int regularId);
}
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public interface IRegularExpenseRepository
{
	Task<List<RawRegularExpenseDto>> GetAllRegular(int accountId);
	Task<int> Create(ModifyRegularExpenseDto expense);
	Task<int> Update(int regularId, RawRegularExpenseDto expense);
	Task<bool> Delete(int regularId);
}
using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public class RegularExpenseRepository:IRegularExpenseRepository
{
	public Task<List<RawRegularExpenseDto>> GetAllRegular(int accountId)
	{
		throw new NotImplementedException();
	}

	public Task<int> Create(RawRegularExpenseDto expense)
	{
		throw new NotImplementedException();
	}

	public Task<int> Update(RawRegularExpenseDto expense)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Delete(int regularId)
	{
		throw new NotImplementedException();
	}
}
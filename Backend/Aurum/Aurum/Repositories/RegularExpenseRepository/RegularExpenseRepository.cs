using Aurum.Models.RegularExpenseDto;

namespace Aurum.Repositories.RegularExpenseRepository;

public class RegularExpenseRepository:IRegularExpenseRepository
{
	public Task<List<RegularExpense>> GetAllRegular(int accountId)
	{
		throw new NotImplementedException();
	}

	public Task<int> Create(RegularExpense expense)
	{
		throw new NotImplementedException();
	}

	public Task<int> Update(RegularExpense expense)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Delete(int regularId)
	{
		throw new NotImplementedException();
	}
}
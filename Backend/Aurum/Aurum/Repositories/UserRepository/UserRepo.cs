using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;
using Microsoft.Identity.Client;

namespace Aurum.Repositories.UserRepository;

public class UserRepo : IUserRepo
{
	public Task<User> Get(int userId)
	{
		throw new NotImplementedException();
	}

	public Task<int> Create(User user)
	{
		throw new NotImplementedException();
	}

	public Task<int> Update(int userId, User user)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Delete(int userId)
	{
		throw new NotImplementedException();
	}
}
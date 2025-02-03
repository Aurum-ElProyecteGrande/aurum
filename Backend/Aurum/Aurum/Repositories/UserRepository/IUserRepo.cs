using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Models.UserDto;

namespace Aurum.Repositories.UserRepository
{
    public interface IUserRepo
    {
        Task<User> Get(int userId);
        Task<int> Create(User user);
        Task<int> Update(int userId, User user);
        Task<bool> Delete(int userId);
    }
}
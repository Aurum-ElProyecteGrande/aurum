using Aurum.Models.AccountDto;
using Aurum.Models.UserDto;

namespace Aurum.Repositories.UserRepository
{
    public interface IUserRepo
    {
        Task<UserDto> Get(int userId);
        Task<int> Create(ModifyUserDto user);
        Task<int> Update(int userId, ModifyUserDto user);
        Task<bool> Delete(int userId);
    }
}
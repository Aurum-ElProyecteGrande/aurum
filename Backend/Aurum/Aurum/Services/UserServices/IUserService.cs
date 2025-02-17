using Aurum.Data.Contracts;
using Aurum.Data.Entities;

namespace Aurum.Services.UserServices
{
    public interface IUserService
    {
        Task<User> Get(int userId);
        Task<int> Create(User user);
        Task<int> Update(User user);
        Task<bool> Delete(int userId);
        Task<AuthResult> RegisterAsync(string email, string username, string password, string role);
        Task<AuthResult> LoginAsync(string email, string password);
    }
}

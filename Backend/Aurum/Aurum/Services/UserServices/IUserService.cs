using Aurum.Data.Contracts;
using Aurum.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Aurum.Services.UserServices
{
    public interface IUserService
    {
        Task<bool> Update(User user);
        Task<bool> Delete(string userId);
        Task<AuthResult> RegisterAsync(string email, string username, string password, string role);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<IdentityResult> ChangePasswordAsync(string userId, PasswordChangeRequest passwordChangeRequest);
    }
}

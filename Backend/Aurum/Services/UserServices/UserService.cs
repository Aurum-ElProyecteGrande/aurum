using Aurum.Data.Contracts;
using Aurum.Data.Entities;
using Aurum.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;

namespace Aurum.Services.UserServices
{
    public class UserService(IUserRepo userRepo, UserManager<IdentityUser> userManager, ITokenService tokenService)  : IUserService
    {
        private IUserRepo _userRepo = userRepo;
        
        public async Task<AuthResponse> GetUserInfo(string userId)
        {
            
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) throw new ArgumentException($"Could not find user with id: {userId}");

            return new AuthResponse(user.Email, user.UserName);
        }
        public async Task<bool> Update(AuthResponse user, string userId)
        {
            var updatedUser = await userManager.FindByIdAsync(userId);
    
            if (updatedUser == null)
                throw new ArgumentException($"Could not find user with id: {userId}");

            updatedUser.Email = user.Email;
            updatedUser.UserName = user.UserName;
            
            var result = await userManager.UpdateAsync(updatedUser);
            
            if (result.Succeeded)
                return true;
            
            throw new InvalidOperationException($"Failed to update user with ID {userId}");
        }
        
        public async Task<bool> Delete(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
    
            if (user == null )
                throw new InvalidOperationException($"Failed to delete account with ID {userId}.");
            
            var result = await userManager.DeleteAsync(user);

            if (result.Succeeded)
                return true;
            
            throw new InvalidOperationException($"Failed to delete user with ID {userId}");
        }
        
        public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            var result = await userManager.CreateAsync( user
                , password);

            if (!result.Succeeded)
                return FailedRegistration(result, email, username);
		
            await userManager.AddToRoleAsync(user, role);

            return new AuthResult(true, email, username, "", user.Id);
        }

        private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
        {
            var authResult = new AuthResult(false, email, username, "", "");

            foreach (var error in result.Errors)
                authResult.ErrorMessages.Add(error.Code, error.Description);
		

            return authResult;
        }
	
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var managedUser = await userManager.FindByEmailAsync(email);

            if (managedUser == null)
                return InvalidEmail(email);
            

            var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, password);
		
            if (!isPasswordValid)
                return InvalidPassword(email, managedUser.UserName);
            

            var roles = await userManager.GetRolesAsync(managedUser);
            var accessToken = tokenService.CreateToken(managedUser,roles[0]);

            return new AuthResult(true, managedUser.Email, managedUser.UserName, accessToken, managedUser.Id);
        }
        
        public async Task<IdentityResult> ChangePasswordAsync(string userId, PasswordChangeRequest passwordChangeRequest)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) 
                throw new InvalidOperationException("User not found.");
            
            
            var result = await userManager.CheckPasswordAsync(user, passwordChangeRequest.OldPassword);
            if (!result)
                return IdentityResult.Failed(new IdentityError { Description = "Incorrect old password." });
            
            
            var changePasswordResult = await userManager.ChangePasswordAsync(user, passwordChangeRequest.OldPassword, passwordChangeRequest.NewPassword);
            return changePasswordResult;
        }

        private static AuthResult InvalidEmail(string email)
        {
            var result = new AuthResult(false, email, "", "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid email");
            return result;
        }

        private static AuthResult InvalidPassword(string email, string userName)
        {
            var result = new AuthResult(false, email, userName, "", "");
            result.ErrorMessages.Add("Bad credentials", "Invalid password");
            return result;
        }
    }
}

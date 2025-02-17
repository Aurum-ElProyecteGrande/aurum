using Aurum.Data.Contracts;
using Aurum.Data.Entities;
using Aurum.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;

namespace Aurum.Services.UserServices
{
    public class UserService(IUserRepo userRepo, UserManager<IdentityUser> userManager, ITokenService tokenService)  : IUserService
    {
        private IUserRepo _userRepo = userRepo;
        
        public async Task<User> Get(int userId)
        {
            if (userId == 0) throw new NullReferenceException("User ID can not be 0");

            var user = await _userRepo.Get(userId);

            if (user == null) throw new ArgumentException($"Could not find user with id: {userId}");

            return user;
        }
        public async Task<int> Create(User user)
        {
            var userId = await _userRepo.Create(user);

            if (userId == 0) throw new Exception("Error while creating user");

            return userId;
        }
        public async Task<int> Update(User user)
        {
            var userId = await _userRepo.Update(user);

            if (userId == 0) throw new Exception("Error while updating user");

            return userId;

        }
        public async Task<bool> Delete(int userId)
        {
            var isDeleted = await _userRepo.Delete(userId);

            if (!isDeleted) throw new InvalidOperationException($"Failed to delete account with ID {userId}.");

            return isDeleted;
        }
        
        public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            var result = await userManager.CreateAsync( user
                , password);

            if (!result.Succeeded)
                return FailedRegistration(result, email, username);
		
            await userManager.AddToRoleAsync(user, role);
            return new AuthResult(true, email, username, "", "");
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

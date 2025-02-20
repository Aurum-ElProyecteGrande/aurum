using Aurum.Data.Seeders.DataReaders;
using Microsoft.AspNetCore.Identity;

namespace Aurum.Data.Seeders.DataGenerators
{
    public class UserGenerator
    {
        private UserManager<IdentityUser> _userManager;
        private List<IdentityUser> _users;

        const string userPw = "user123";
        const string userRole = "User";
        const string adminPw = "admin123";
        const string adminRole = "Admin";

        public UserGenerator(UserManager<IdentityUser> userManager, List<IdentityUser> users)
        {
            _userManager = userManager;
            _users = users;
        }


        public async Task GenerateUsers()
        {
            if (await HasUser()) return;

            foreach (var user in _users)
            {
                var userCreated = await _userManager.CreateAsync(user, userPw);
                if (userCreated.Succeeded) await _userManager.AddToRoleAsync(user, userRole);
            }
        }

        public async Task GenerateAdmin()
        {
            if (await HasAdmin()) return;
            var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
            var adminCreated = await _userManager.CreateAsync(admin, adminPw);

            if (adminCreated.Succeeded)
                await _userManager.AddToRoleAsync(admin, adminRole);
        }

        private async Task<bool> HasUser()
        {
            foreach (var user in _users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(userRole)) return true;
            }
            return false;
        }

        private async Task<bool> HasAdmin()
        {
            foreach (var user in _users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(adminRole)) return true;
            }
            return false;
        }
    }
}


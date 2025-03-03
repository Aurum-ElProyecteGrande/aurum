using Microsoft.AspNetCore.Identity;

namespace Aurum.Data.Seeders;

public class AuthenticationSeeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
{
	private RoleManager<IdentityRole> _roleManager = roleManager;
	private UserManager<IdentityUser> _userManager = userManager;
	
	public void AddRoles()
	{
		var tAdmin = CreateAdminRole(roleManager);
		tAdmin.Wait();

		var tUser = CreateUserRole(roleManager);
		tUser.Wait();
	}

    public async Task CreateAdminIfNotExists()
    {
        var adminInDb = await userManager.FindByEmailAsync("admin@admin.com");
        if (adminInDb == null)
        {
            var admin = new IdentityUser { UserName = "admin", Email = "admin@admin.com" };
            var adminCreated = await userManager.CreateAsync(admin, "admin123");

            if (adminCreated.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }

    private async Task CreateAdminRole(RoleManager<IdentityRole> manager) =>
		await manager.CreateAsync(new IdentityRole("Admin"));

	private async Task CreateUserRole(RoleManager<IdentityRole> manager) =>
		await manager.CreateAsync(new IdentityRole("User"));

}
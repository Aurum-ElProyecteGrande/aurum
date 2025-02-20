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
		
	private async Task CreateAdminRole(RoleManager<IdentityRole> manager) =>
		await manager.CreateAsync(new IdentityRole("Admin"));

	async Task CreateUserRole(RoleManager<IdentityRole> manager) =>
		await manager.CreateAsync(new IdentityRole("User"));
	

	

}
using Microsoft.AspNetCore.Identity;

namespace Aurum.Services.UserServices;

public interface ITokenService
{
	public string CreateToken(IdentityUser user, string role);
}
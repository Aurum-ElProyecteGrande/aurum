using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Utils;

public static class UserHelper
{
	public static bool GetUserId(HttpContext context, out string? userId, out IActionResult? unauthorized)
	{
		userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

		if (userId != null)
		{
			unauthorized = null;
			return false;
		}

		unauthorized = new UnauthorizedResult();
		return true;
	}
}
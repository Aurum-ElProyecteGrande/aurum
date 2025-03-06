using System.Security.Claims;
using Aurum.Services.AccountService;

namespace Aurum.Middleware;

public class AccountValidationMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context, IAccountService accountService)
	{
		var accountId = context.GetRouteValue("accountId")?.ToString();

		if (string.IsNullOrEmpty(accountId) || !int.TryParse(accountId, out var accountIdInt))
		{
			await next(context);
			return;
		}

		var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


		var isValid = await accountService.ValidCheck(userId, accountIdInt);

		if (!isValid)
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			await context.Response.WriteAsync("Forbidden: Account mismatch.");
			return;
		}


		await next(context);
	}

}

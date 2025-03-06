using Aurum.Services.ExpenseCategoryService;
using Aurum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.ExpenseCategoryController;

[Authorize]
public class ExpenseCategoryController(IExpenseCategoryService service, ILogger<ExpenseCategoryController> logger): ControllerBase
{
	private readonly IExpenseCategoryService _service = service;
	private readonly ILogger<ExpenseCategoryController> _logger  = logger;

	[HttpGet("/categories/expense")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized))
				return unauthorized;

			var categories = await _service.GetAllExpenseCategories(userId);

			return Ok(categories);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "An error occurred while fetching expense categories.");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}

	}
}

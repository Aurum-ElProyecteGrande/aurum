using Aurum.Services.ExpenseCategoryService;
using Aurum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.ExpenseCategoryController;

[Authorize]
public class ExpenseCategoryController(IExpenseCategoryService service): ControllerBase
{
	private readonly IExpenseCategoryService _service = service;
	
	[HttpGet("/categories/expense")]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized)) 
				return unauthorized;
			
			var categories = await _service.GetAllExpenseCategories(userId);
			// if (categories.Count == 0)
			// 	throw new InvalidOperationException("No expense categories found");
			
			return Ok(categories);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
		
	}
}
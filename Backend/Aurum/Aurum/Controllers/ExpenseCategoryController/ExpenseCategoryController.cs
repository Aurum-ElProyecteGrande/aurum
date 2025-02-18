using Aurum.Services.ExpenseCategoryService;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.ExpenseCategoryController;

public class ExpenseCategoryController(IExpenseCategoryService service): ControllerBase
{
	private readonly IExpenseCategoryService _service = service;
	
	[HttpGet("/categories/expense/{userId}")]
	public async Task<IActionResult> GetAll([FromRoute] string userId)
	{
		try
		{
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
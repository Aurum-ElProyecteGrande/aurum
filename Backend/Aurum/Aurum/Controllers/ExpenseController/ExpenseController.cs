using System.Globalization;
using Aurum.Models.ExpenseDto;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.ExpenseController;

public class ExpenseController(IExpenseService service):ControllerBase
{
	private readonly IExpenseService _service = service;

	//TODO userId should be replaced from the authentication context
	[HttpGet("/expenses/{accountId:int}")]
	public async Task<IActionResult> GetAll([FromRoute]int accountId, [FromQuery]int userId)
	{
		try
		{
			var expenses = await _service.GetAll(accountId, userId);
			return Ok(expenses);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}
	
	//TODO userId should be replaced from the authentication context
	[HttpGet("/expenses/{accountId:int}/{startDate:datetime}/{endDate:datetime}")]
	public async Task<IActionResult> GetAllWithDate([FromRoute]int accountId, [FromRoute]DateTime startDate, [FromRoute]DateTime endDate, [FromQuery]int userId)
	{
		try
		{
			var expenses = await _service.GetAll(accountId, userId, startDate, endDate);
			return Ok(expenses);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}

	[HttpPost("/expenses")]
	public async Task<IActionResult> Create([FromBody]ModifyExpenseDto expense)
	{
		try
		{
			var id = await _service.Create(expense);
			return Ok(id);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}

	[HttpDelete("/expenses/{expenseId:int}")]
	public async Task<IActionResult> Delete([FromRoute]int expenseId)
	{
		try
		{
			var isDeleted = await _service.Delete(expenseId);
			return Ok(isDeleted);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}
}
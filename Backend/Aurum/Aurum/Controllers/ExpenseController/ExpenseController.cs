using Aurum.Models.ExpenseDto;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.ExpenseController;

public class ExpenseController(IExpenseService service):ControllerBase
{
	private readonly IExpenseService _service = service;

	[HttpGet("/expenses/{accountId:int}&{userId:int}")]
	public async Task<IActionResult> GetAll([FromRoute]int accountId, [FromRoute]int userId)
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
	[HttpGet("/expenses/{accountId:int}&{userId:int}&{startDate:DateTime}&{endDate:DateTime}")]
	public async Task<IActionResult> GetAll([FromRoute]int accountId, [FromRoute]int userId, [FromRoute]DateTime startDate, [FromRoute]DateTime endDate)
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
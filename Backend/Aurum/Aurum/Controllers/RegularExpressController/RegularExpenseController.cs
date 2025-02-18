using Aurum.Models.RegularExpenseDto;
using Aurum.Services.RegularExpenseService;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.RegularExpressController;

public class RegularExpenseController(IRegularExpenseService service):ControllerBase
{
	private readonly IRegularExpenseService _service = service;

	//TODO userId should be replaced from the authentication context
	[HttpGet("/expenses/regulars/{accountId:int}")]
	public async Task<IActionResult> GetAll([FromRoute]int accountId, [FromQuery]string userId)
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

	[HttpPost("/expenses/regulars/")]
	public async Task<IActionResult> Create([FromQuery]int regularId,[FromBody]ModifyRegularExpenseDto expense)
	{
		try
		{
			var id = await _service.Create(regularId,expense);
			return Ok(id);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}
	
	[HttpPut("/expenses/regulars/{regularId:int}")]
	public async Task<IActionResult> Update([FromRoute]int regularId,[FromBody]ModifyRegularExpenseDto expense)
	{
		try
		{
			var id = await _service.Update(regularId, expense);
			return Ok(id);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}

	[HttpDelete("/expenses/regulars/{regularId:int}")]
	public async Task<IActionResult> Delete([FromRoute]int regularId)
	{
		try
		{
			var isDeleted = await _service.Delete(regularId);
			return Ok(isDeleted);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			return StatusCode(500, e.Message);
		}
	}
}
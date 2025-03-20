using Aurum.Models.RegularExpenseDto;
using Aurum.Services.RegularExpenseService;
using Aurum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.RegularExpressController;

[Authorize]
public class RegularExpenseController(IRegularExpenseService service, ILogger<RegularExpenseController> logger):ControllerBase
{
	private readonly IRegularExpenseService _service = service;
	private readonly ILogger<RegularExpenseController> _logger = logger;
	
	[HttpGet("/expenses/regulars/{accountId:int}")]
	public async Task<IActionResult> GetAll([FromRoute]int accountId)
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized))
				return unauthorized;

			var expenses = await _service.GetAllWithId(accountId);
			return Ok(expenses);
		}
		catch (Exception e)
		{
			_logger.LogError($"An error occured while getting regular expenses: {e.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}

	[HttpPost("/expenses/regulars")]
	public async Task<IActionResult> Create([FromBody]ModifyRegularExpenseDto expense)
	{
		try
		{
			var id = await _service.Create(expense);
			return Ok(id);
		}
		catch (Exception e)
		{
			_logger.LogError($"An error occured while creating regular expenses: {e.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
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
			_logger.LogError($"An error occured while updating regular expenses: {e.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
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
			_logger.LogError($"An error occured while deleting regular expenses: {e.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}
}

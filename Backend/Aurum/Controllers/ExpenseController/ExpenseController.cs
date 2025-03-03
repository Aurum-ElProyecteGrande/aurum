using System.Globalization;
using System.Security.Claims;
using Aurum.Models.ExpenseDto;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;
using Aurum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.ExpenseController;

[Authorize]
public class ExpenseController(IExpenseService service, ILogger<ExpenseController> logger):ControllerBase
{
	private readonly IExpenseService _service = service;
	private readonly ILogger<ExpenseController> _logger = logger;
	
	[HttpGet("/expenses/{accountId:int}")]
	public async Task<IActionResult> GetAll([FromRoute]int accountId)
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized)) 
				return unauthorized;

			var expenses = await _service.GetAll(accountId);
			return Ok(expenses);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "An error occurred while fetching expenses for accountId {AccountId}.", accountId);
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}
	
	[HttpGet("/expenses/{accountId:int}/{startDate:datetime}/{endDate:datetime}")]
	public async Task<IActionResult> GetAllWithDate([FromRoute]int accountId, [FromRoute]DateTime startDate, [FromRoute]DateTime endDate)
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized)) 
				return unauthorized;
			
			var expenses = await _service.GetAll(accountId, startDate, endDate);
			return Ok(expenses);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "An error occurred while fetching expenses for accountId {AccountId} from {StartDate} to {EndDate}.", accountId, startDate, endDate);;
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}

    [HttpGet("/expenses/currency/{accountId:int}")]
    public async Task<IActionResult> GetAllWithCurrency([FromRoute] int accountId)
    {
        try
        {
            if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
                return unauthorized;

            var expenses = await _service.GetAllWithCurrency(accountId);
            return Ok(expenses);
        }
        catch (Exception e)
        {
	        _logger.LogError(e, "An error occurred while fetching expenses for accountId {AccountId}.", accountId);
	        return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
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
			_logger.LogError(e, "An error occurred while creating expense");

			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
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
			_logger.LogError(e, "An error occurred while deleting expense with: {ExpenseId} id.", expenseId);
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}
}
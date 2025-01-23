using Microsoft.AspNetCore.Mvc;
using Aurum.Services.BalanceService;
using Aurum.Models.IncomeDTOs;

namespace Aurum.Controllers.BallanceController
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController : ControllerBase
    {
        IBalanceService _balanceService;
        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpGet("{accountId:int}")]
        public async Task<IActionResult> GetBalance(int accountId, [FromQuery] DateTime? date)
        {
            try
            {
                decimal balance = 0;

                if (date is not null)
                {
                    var validDate = _balanceService.ValidateDate(date);
                    balance = await _balanceService.GetBalance(accountId, validDate);
                }
                else
                {
                    balance = await _balanceService.GetBalance(accountId);
                }

                return Ok(balance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

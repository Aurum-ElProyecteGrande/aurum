using Microsoft.AspNetCore.Mvc;
using Aurum.Services.BalanceService;
using Aurum.Models.IncomeDTOs;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aurum.Controllers.BalanceController
{
    [ApiController]
    [Authorize]
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

        [HttpGet("{accountId:int}/range")]
        public async Task<IActionResult> GetBalanceForRange(int accountId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                if (startDate is null || endDate is null)
                {
                    decimal balance = await _balanceService.GetBalance(accountId);
                    return Ok(balance);
                }

                var validStartDate = _balanceService.ValidateDate(startDate);
                var validEndDate = _balanceService.ValidateDate(endDate);

                var balances = _balanceService.GetBalanceForRange(accountId, validStartDate, validEndDate);

                return Ok(balances);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

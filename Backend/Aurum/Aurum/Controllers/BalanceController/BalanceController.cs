using Microsoft.AspNetCore.Mvc;
using Aurum.Services.BallanceService;
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
        public async Task<IActionResult> GetBallance(int accountId, [FromQuery] DateTime? date)
        {
            try
            {
                decimal ballance = 0;

                if (date is not null)
                {
                    var validDate = _balanceService.ValidateDate(date);
                    ballance = await _balanceService.GetBalance(accountId, validDate);
                }
                else
                {
                    ballance = await _balanceService.GetBalance(accountId);
                }

                return Ok(ballance);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

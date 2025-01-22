using Microsoft.AspNetCore.Mvc;
using Aurum.Services.BallanceService;
using Aurum.Models.IncomeDTOs;

namespace Aurum.Controllers.BallanceController
{
    [ApiController]
    [Route("[controller]")]
    public class BallanceController : ControllerBase
    {
        IBallanceService _ballanceService;
        public BallanceController(IBallanceService ballanceService)
        {
            _ballanceService = ballanceService;
        }

        [HttpGet("{accountId:int}")]
        public async Task<IActionResult> GetBallance(int accountId, [FromQuery] DateTime? date)
        {
            try
            {
                decimal ballance = 0;

                if (date is not null)
                {
                    var validDate = _ballanceService.ValidateDate(date);
                    ballance = await _ballanceService.GetBallance(accountId, validDate);
                }
                else
                {
                    ballance = await _ballanceService.GetBallance(accountId);
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

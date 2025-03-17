using Microsoft.AspNetCore.Mvc;
using Aurum.Models.IncomeDTOs;
using System;
using Aurum.Services.IncomeServices;
using Aurum.Services.RegularIncomeServices;
using Aurum.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace Aurum.Controllers.IncomeControllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeService _incomeService;
        private readonly IRegularIncomeService _regularIncomeService;
		private readonly ILogger<IncomeController> _logger;
		public IncomeController(IIncomeService incomeService, IRegularIncomeService regularIncomeService, ILogger<IncomeController> logger)
        {
            _incomeService = incomeService;
            _regularIncomeService = regularIncomeService;
			_logger = logger;
        }


        [HttpGet("{accountId:int}")]
        public async Task<IActionResult> GetAll(int accountId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                List<IncomeDto> incomes = new();

                if (startDate is not null && endDate is not null)
                {
                    var (validStartDate, validEndDate) = _incomeService.ValidateDates(startDate, endDate);
                    incomes = await _incomeService.GetAll(accountId, validStartDate, validEndDate);
                }
                else
                {
                    incomes = await _incomeService.GetAll(accountId);
                }

                return Ok(incomes);
            }
            catch (Exception ex)
			{
				_logger.LogError($"An error occured while getting incomes for acc#{accountId}: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModifyIncomeDto income)
        {
            try
            {
                var incomeId = await _incomeService.Create(income);
                return Ok(incomeId);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while creating income {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpDelete("{incomeId:int}")]
        public async Task<IActionResult> Delete(int incomeId)
        {
            try
            {
                var isDeleted = await _incomeService.Delete(incomeId);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while deleting income#{incomeId}: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");

			}
        }

        [HttpGet("regulars/{accountId:int}")]
        public async Task<IActionResult> GetAllRegular(int accountId)
        {
            try
            {
                var regularIncomes = _regularIncomeService.GetAllRegular(accountId);
                return Ok(regularIncomes);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while getting regular incomes for acc#{accountId}: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpPost("regulars")]
        public async Task<IActionResult> CreateRegular(ModifyRegularIncomeDto regularIncome)
        {
            try
            {
                var regularIncomeId = await _regularIncomeService.CreateRegular(regularIncome);
                return Ok(regularIncomeId);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while creating regular income: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpPut("regulars/{regularId}")]
        public async Task<IActionResult> UpdateRegular(ModifyRegularIncomeDto regularIncome, int regularId)
        {
            try
            {
                var regularIncomeId = await _regularIncomeService.UpdateRegular(regularIncome, regularId);
                return Ok(regularIncomeId);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while updating regular income for regular income#{regularId}: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpDelete("regulars/{regularId:int}")]
        public async Task<IActionResult> DeleteRegular(int regularId)
        {
            try
            {
                var isDeleted = await _regularIncomeService.DeleteRegular(regularId);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while deleting regular income#{regularId}: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }
    }
}

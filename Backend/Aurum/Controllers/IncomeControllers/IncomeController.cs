using Microsoft.AspNetCore.Mvc;
using Aurum.Models.IncomeDTOs;
using System;
using Aurum.Services.IncomeServices;
using Aurum.Services.RegularIncomeServices;
using Aurum.Data.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Aurum.Controllers.IncomeControllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private IIncomeService _incomeService;
        private IRegularIncomeService _regularIncomeService;
        public IncomeController(IIncomeService incomeService, IRegularIncomeService regularIncomeService)
        {
            _incomeService = incomeService;
            _regularIncomeService = regularIncomeService;
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("regulars/{accountId:int}")]
        public async Task<IActionResult> GetAllRegular(int accountId)
        {
            try
            {
                var regularIncomes = _regularIncomeService.GetAllRegularWithId(accountId);
                return Ok(regularIncomes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

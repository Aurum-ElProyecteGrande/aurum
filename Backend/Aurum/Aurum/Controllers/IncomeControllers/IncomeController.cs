using Microsoft.AspNetCore.Mvc;
using Aurum.Models.IncomeDTOs;
using System;
using Aurum.Services.IncomeServices;
using Aurum.Services.RegularIncomeServices;
using Aurum.Data.Entities;

namespace Aurum.Controllers.IncomeControllers
{
    [ApiController]
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
                List<Data.Entities.Income> incomes = new();

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
        public async Task<IActionResult> Create(Data.Entities.Income income)
        {
            try
            {
                var incomeId = await _incomeService.Create(income);

                if (incomeId == 0) throw new InvalidOperationException ("Invalid income input");

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

                if (!isDeleted) throw new InvalidOperationException ($"Could not delete income with id {incomeId}");

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
                var regularIncomes = _regularIncomeService.GetAllRegular(accountId);

                return Ok(regularIncomes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("regulars")]
        public async Task<IActionResult> CreateRegular(RegularIncome regularIncome)
        {
            try
            {
                var regularIncomeId = await _regularIncomeService.CreateRegular(regularIncome);

                if (regularIncomeId == 0) throw new InvalidOperationException ("Invalid regular income input");

                return Ok(regularIncomeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("regulars")]
        public async Task<IActionResult> UpdateRegular(RegularIncome regularIncome)
        {
            try
            {
                var regularIncomeId = await _regularIncomeService.UpdateRegular(regularIncome);

                if (regularIncomeId == 0) throw new InvalidOperationException ("Invalid regular income input");

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

                if (!isDeleted) throw new InvalidOperationException ($"Could not delete income with id {regularId}");

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
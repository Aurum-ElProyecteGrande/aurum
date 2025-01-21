using Microsoft.AspNetCore.Mvc;
using Aurum.Models.IncomeDTOs;
using System;
using Aurum.Repositories.Income;

namespace Aurum.Controllers.Income
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private IIncomeRepo _incomeRepo;
        private IRegularIncomeRepo _regularIncomeRepo;
        public IncomeController(IIncomeRepo incomeRepo, IRegularIncomeRepo regularIncomeRepo)
        {
            _incomeRepo = incomeRepo;
            _regularIncomeRepo = regularIncomeRepo;
        }


        [HttpGet("{accountId:int}")]
        public async Task<IActionResult> GetAllByDate(int accountId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                List<IncomeDto> incomes = new();

                if (startDate is not null && endDate is not null)
                {
                    incomes = await _incomeRepo.GetAll(accountId, startDate, endDate);
                }
                else
                {
                    incomes = await _incomeRepo.GetAll(accountId);
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
                var incomeId = await _incomeRepo.Create(income);

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
                var isDeleted = await _incomeRepo.Delete(incomeId);

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
                var regularIncomes = _regularIncomeRepo.GetAllRegular(accountId);

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
                var regularIncomeId = await _regularIncomeRepo.CreateRegular(regularIncome);

                if (regularIncomeId == 0) throw new InvalidOperationException ("Invalid regular income input");

                return Ok(regularIncomeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("regulars/{regularId:int}")]
        public async Task<IActionResult> UpdateRegular(int regularId, ModifyRegularIncomeDto regularIncome)
        {
            try
            {
                var regularIncomeId = await _regularIncomeRepo.UpdateRegular(regularId, regularIncome);

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
                var isDeleted = await _regularIncomeRepo.DeleteRegular(regularId);

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
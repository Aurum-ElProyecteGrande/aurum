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
        private ILogger<IncomeController> _logger;
        public IncomeController(IIncomeRepo incomeRepo, IRegularIncomeRepo regularIncomeRepo, ILogger<IncomeController> logger)
        {
            _incomeRepo = incomeRepo;
            _regularIncomeRepo = regularIncomeRepo;
            _logger = logger;
        }


        [HttpGet("{accountId:int}")]
        public ActionResult<List<IncomeDto>> GetAllByDate(int accountId, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            try
            {
                List<IncomeDto> incomes = new();

                if (startDate is not null && endDate is not null)
                {
                    incomes = _incomeRepo.GetAll(accountId, startDate, endDate);
                }
                else
                {
                    incomes = _incomeRepo.GetAll(accountId);
                }

                return Ok(incomes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<int> Create(ModifyIncomeDto income)
        {
            try
            {
                var incomeId = _incomeRepo.Create(income);

                if (incomeId == 0) return BadRequest("Invalid income input");

                return Ok(incomeId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{incomeId:int}")]
        public ActionResult<bool> Delete(int incomeId)
        {
            try
            {
                var isDeleted = _incomeRepo.Delete(incomeId);

                if (!isDeleted) return BadRequest($"Could not delete income with id {incomeId}");

                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("regulars/{accountId:int}")]
        public ActionResult<List<RegularIncomeDto>> GetAllRegular(int accountId)
        {
            try
            {
                var regularIncomes = _regularIncomeRepo.GetAllRegular(accountId);

                return Ok(regularIncomes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPost("regulars")]
        public ActionResult<int> CreateRegular(ModifyRegularIncomeDto regularIncome)
        {
            try
            {
                var regularIncomeId = _regularIncomeRepo.CreateRegular(regularIncome);

                if (regularIncomeId == 0) return BadRequest("Invalid regular income input");

                return Ok(regularIncomeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPut("regulars/{regularId:int}")]
        public ActionResult<int> UpdateRegular(int regularId, ModifyRegularIncomeDto regularIncome)
        {
            try
            {
                var regularIncomeId = _regularIncomeRepo.UpdateRegular(regularId, regularIncome);

                if (regularIncomeId == 0) return BadRequest("Invalid regular income input");

                return Ok(regularIncomeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("regulars/{regularId:int}")]
        public ActionResult<bool> DeleteRegular(int regularId)
        {
            try
            {
                var isDeleted = _regularIncomeRepo.DeleteRegular(regularId);

                if (!isDeleted) return BadRequest($"Could not delete income with id {regularId}");

                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);

            }
        }
    }
}
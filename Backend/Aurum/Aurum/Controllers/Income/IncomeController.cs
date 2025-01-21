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

        [HttpGet("{accountId}")]
        public ActionResult<List<IncomeDto>> GetAll(int accountId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{accountId}/bydate")]
        public ActionResult<List<IncomeDto>> GetAllByDate(int accountId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult<int> Create(ModifyIncomeDto income)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{incomeId}")]
        public ActionResult<bool> Delete(int incomeId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("regulars/{accountId}")]
        public ActionResult<List<RegularIncomeDto>> GetAllRegular(int accountId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("regulars")]
        public ActionResult<int> CreateRegular(ModifyRegularIncomeDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("regulars/{regularId}")]
        public ActionResult<int> UpdateRegular(int regularId, ModifyRegularIncomeDto regularIncome)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("regulars/{regularId}")]
        public ActionResult<bool> DeleteRegular(int regularId)
        {
            throw new NotImplementedException();
        }
    }

}

}

}


}

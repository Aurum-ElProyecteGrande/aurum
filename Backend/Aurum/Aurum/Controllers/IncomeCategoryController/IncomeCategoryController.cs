using Aurum.Models.CategoryDTOs;
using Aurum.Repositories.Income;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.Categories
{
    [ApiController]
    public class IncomeCategoryController : ControllerBase
    {
        private IIncomeCategoryRepo _incomeCategoryRepo;
        private ILogger<IncomeCategoryController> _logger;

        public IncomeCategoryController(IIncomeCategoryRepo incomeCategoryRepo, ILogger<IncomeCategoryController> logger)
        {
            _incomeCategoryRepo = incomeCategoryRepo;
            _logger = logger;
        }

        [HttpGet("/categories/expense")]
        public ActionResult<List<CategoryDto>> GetAll()
        {
            try
            {
                var incomeCategories = _incomeCategoryRepo.GetAllCategory();

                return Ok(incomeCategories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
            throw new NotImplementedException();
        }
    }

}


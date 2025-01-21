using Aurum.Models.CategoryDTOs;
using Aurum.Repositories.Income;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.Categories
{
    [ApiController]
    [Route("/categories/income")]
    public class IncomeCategoryController : ControllerBase
    {
        private IIncomeCategoryRepo _incomeCategoryRepo;

        public IncomeCategoryController(IIncomeCategoryRepo incomeCategoryRepo)
        {
            _incomeCategoryRepo = incomeCategoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAll()
        {
            try
            {
                var incomeCategories = await _incomeCategoryRepo.GetAllCategory();

                return Ok(incomeCategories);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }

}


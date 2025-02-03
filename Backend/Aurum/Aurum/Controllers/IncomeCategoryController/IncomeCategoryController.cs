using Aurum.Models.CategoryDtos;
using Aurum.Repositories.IncomeRepository.IncomeCategoryRepository;
using Aurum.Services.IncomeCategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.IncomeCategoriyControllers
{
    [ApiController]
    [Route("/categories/income")]
    public class IncomeCategoryController : ControllerBase
    {
        private IIncomeCategoryService _incomeCategoryService;

        public IncomeCategoryController(IIncomeCategoryService incomeCategoryService)
        {
            _incomeCategoryService = incomeCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var incomeCategories = await _incomeCategoryService.GetAllCategory();

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


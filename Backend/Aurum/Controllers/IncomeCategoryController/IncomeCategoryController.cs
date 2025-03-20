using Aurum.Models.CategoryDtos;
using Aurum.Repositories.IncomeRepository.IncomeCategoryRepository;
using Aurum.Services.IncomeCategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.IncomeCategoriyControllers
{
    [ApiController]
    [Authorize]
    [Route("/categories/income")]
    public class IncomeCategoryController : ControllerBase
    {
        private readonly IIncomeCategoryService _incomeCategoryService;
		private readonly ILogger<IncomeCategoryController> _logger;

		public IncomeCategoryController(IIncomeCategoryService incomeCategoryService, ILogger<IncomeCategoryController> logger)
        {
            _incomeCategoryService = incomeCategoryService;
			_logger = logger;
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
				_logger.LogError($"An error occured while getting income categories: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }
    }

}


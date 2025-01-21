using Aurum.Models.CategoryDTOs;
using Aurum.Repositories.Income;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.Categories
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private IIncomeCategoryRepo _incomeCategoryRepo;
        private ILogger<CategoriesController> _logger;

        public CategoriesController(IIncomeCategoryRepo incomeCategoryRepo, ILogger<CategoriesController> logger)
        {
            _incomeCategoryRepo = incomeCategoryRepo;
            _logger = logger;
        }

        [HttpGet("/categories/expense/{userId:int}")]
        public ActionResult<List<CategoryDto>> GetAll(int userID)
        {
            throw new NotImplementedException();
        }
    }

}


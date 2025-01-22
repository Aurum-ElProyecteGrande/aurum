using Aurum.Repositories.CurrencyRepository;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.CurrencyController;

    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepo _currencyRepo;

        public CurrencyController(ICurrencyRepo currencyRepo)
        {
            _currencyRepo = currencyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var currencies = await _currencyRepo.GetAll();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
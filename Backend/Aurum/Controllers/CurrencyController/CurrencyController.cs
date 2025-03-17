using Aurum.Repositories.CurrencyRepository;
using Aurum.Services.CurrencyServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.CurrencyController;

[ApiController]
[Authorize]
[Route("[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;
	private readonly ILogger<CurrencyController> _logger;
	public CurrencyController(ICurrencyService currencyService, ILogger<CurrencyController> logger)
    {
        _currencyService = currencyService;
		_logger = logger;
	}

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        { 
            var currencies = await _currencyService.GetAll();
            return Ok(currencies);
        }
        catch (Exception ex)
		{
			_logger.LogError($"An error occured while getting currencies: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
    }
}

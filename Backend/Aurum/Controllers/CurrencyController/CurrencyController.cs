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

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
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
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
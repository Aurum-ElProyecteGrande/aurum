using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.LayoutDTOs;
using Aurum.Services.LayoutServices;
using Aurum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Aurum.Controllers.LayoutControllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class LayoutController : ControllerBase
{
    private readonly ILayoutService _layoutService;
	private readonly ILogger<LayoutController> _logger;
    public LayoutController(ILayoutService layoutService, ILogger<LayoutController> logger)
    {
        _layoutService = layoutService;
		_logger = logger;
	}

    [HttpPost("basic")]
    public async Task<IActionResult> CreateBasicLayout([FromBody] LayoutDto layout)
    {
        try
        {
            if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
                return unauthorized;

            if (layout.LayoutName != "basic") BadRequest("wrong layout name");

            var layoutId = await _layoutService.CreateOrUpdateBasic(layout, userId);

            return Ok(layoutId);
        }
		catch (Exception ex)
        {
			_logger.LogError($"An error occured while creating basic layout: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
    }

    [HttpPost("scientic")]
    public async Task<IActionResult> CreateScienticLayout([FromBody] LayoutDto layout)
    {
        try
        {
            if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
                return unauthorized;

            if (layout.LayoutName != "scientic") BadRequest("wrong layout name");

            var layoutId = await _layoutService.CreateOrUpdateScientic(layout, userId);

            return Ok(layoutId);
        }
        catch (Exception ex)
		{
			_logger.LogError($"An error occured while creating scientic layout: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
    }

    [HttpPost("detailed")]
    public async Task<IActionResult> CreateDetailedLayout([FromBody] LayoutDto layout)
    {
        try
        {
            if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
                return unauthorized;

            if (layout.LayoutName != "detailed") BadRequest("wrong layout name");

            var layoutId = await _layoutService.CreateOrUpdateDetailed(layout, userId);

            return Ok(layoutId);
        }
        catch (Exception ex)
		{
			_logger.LogError($"An error occured while creating detailed layout: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
                return unauthorized;

            var allLayout = await _layoutService.GetAll(userId);

            return Ok(allLayout);
        }
        catch (Exception ex)
		{
			_logger.LogError($"An error occured while getting layouts: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
    }
}

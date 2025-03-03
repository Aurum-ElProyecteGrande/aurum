using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Models.LayoutDTOs;
using Aurum.Services.LayoutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.LayoutControllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class LayoutController : ControllerBase
{
    private ILayoutService _layoutService;
    public LayoutController(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    [HttpPost("basic")]
    public async Task<IActionResult> CreateBasicLayout([FromBody] LayoutDto layout)
    {
        try
        {
            if (layout.LayoutName != "basic") BadRequest("wrong layout name");

            var layoutId = await _layoutService.CreateOrUpdateBasic(layout);

            return Ok(layoutId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("scientic")]
    public async Task<IActionResult> CreateScienticLayout([FromBody] LayoutDto layout)
    {
        try
        {
            if (layout.LayoutName != "scientic") BadRequest("wrong layout name");

            var layoutId = await _layoutService.CreateOrUpdateScientic(layout);

            return Ok(layoutId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("detailed")]
    public async Task<IActionResult> CreateDetailedLayout([FromBody] LayoutDto layout)
    {
        try
        {
            if (layout.LayoutName != "detailed") BadRequest("wrong layout name");

            var layoutId = await _layoutService.CreateOrUpdateDetailed(layout);

            return Ok(layoutId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll(string userId)
    {
        try
        {
            var allLayout = await _layoutService.GetAll(userId);

            return Ok(allLayout);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
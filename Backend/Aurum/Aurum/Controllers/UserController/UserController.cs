using Aurum.Data.Entities;
using Aurum.Services.UserServices;
using Aurum.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.UserController;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{userId:int}")]
    public async Task<IActionResult> Get([FromRoute] int userId)
    {
        try
        {
            var user = await _userService.Get(userId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(User user)
    {
        try
        {
            var userId = await _userService.Create(user);

            return Ok(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpPut()]
    public async Task<IActionResult> Update(User user)
    {
        try
        {
            var updatedId = await _userService.Update(user);

            return Ok(updatedId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{userId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int userId)
    {
        try
        {
            var isDeleted = await _userService.Delete(userId);

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }
}
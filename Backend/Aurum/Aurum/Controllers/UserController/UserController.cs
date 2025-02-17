using Aurum.Data.Contracts;
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
    

    [HttpPut()]
    public async Task<IActionResult> Update(User user)
    {
        try
        {
            var didUpdate = await _userService.Update(user);

            return Ok(didUpdate);
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
            var isDeleted = await _userService.Delete(userId.ToString());

            return Ok(isDeleted);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _userService.RegisterAsync(request.Email, request.Username, request.Password, request.Role);

        if (result.Success)
            return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
		
        AddErrors(result);
        return BadRequest(ModelState);

    }
	
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.LoginAsync(request.Email, request.Password);
        
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.UtcNow.AddHours(1)
        };

        Response.Cookies.Append("AuthToken", result.Token, cookieOptions);
        
        if (result.Success) 
            return Ok(new AuthResponse(result.Email, result.UserName, result.UserId));
		
        AddErrors(result);
        return BadRequest(ModelState);
    }


    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
            ModelState.AddModelError(error.Key, error.Value);
    }
}
using Aurum.Data.Contracts;
using Aurum.Data.Entities;
using Aurum.Models.UserDTO;
using Aurum.Services.AccountService;
using Aurum.Services.UserServices;
using Aurum.Utils;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.UserController;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;
	private readonly IAccountService _accountService;
	private readonly ILogger<UserController> _logger;

	public UserController(IUserService userService, ILogger<UserController> logger, IAccountService accountService)
	{
		_userService = userService;
		_accountService = accountService;
		_logger = logger;
	}

	[HttpGet(), Authorize]
	public async Task<IActionResult> GetUserInfo()
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
				return unauthorized;

			var userInfo = await _userService.GetUserInfo(userId);

			return Ok(userInfo);
		}
		catch (Exception ex)
		{
			_logger.LogError($"An error occured while getting user info: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}


	[HttpPut()]
	public async Task<IActionResult> Update([FromBody] AuthResponse user)
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
				return unauthorized;

			var didUpdate = await _userService.Update(user, userId);

			return Ok(didUpdate);
		}
		catch (Exception ex)
		{
			_logger.LogError($"An error occured while updating user: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}

	[HttpDelete("")]
	public async Task<IActionResult> Delete()
	{
		try
		{
			if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
				return unauthorized;

			var isDeleted = await _userService.Delete(userId);

			return Ok(isDeleted);
		}
		catch (Exception ex)
		{
			_logger.LogError($"An error occured while deleting user: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}
	}

	[HttpPost("register")]
	public async Task<ActionResult<RegistrationResponse>> Register(UserCreationDto userCreationDto)
	{
		try
		{

			var regRequest = userCreationDto.RegistrationRequest;
			var newAccount = userCreationDto.ModifyAccountDto;

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _userService.RegisterAsync(regRequest.Email, regRequest.Username, regRequest.Password, regRequest.Role);

			if (!result.Success)
			{
				AddErrors(result);
				return BadRequest(ModelState);
			}

			await _accountService.Create(newAccount, result.UserId);

			return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
		}
		catch (Exception ex)
		{
			_logger.LogError($"An error occured while creating user: {ex.Message}");
			return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
		}


	}

	[HttpPost("login")]
	public async Task<IActionResult> Authenticate([FromBody] AuthRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);


		var result = await _userService.LoginAsync(request.Email, request.Password);

		var cookieOptions = new CookieOptions
		{
			Expires = DateTime.UtcNow.AddHours(1)
		};

		Response.Cookies.Append("AuthToken", result.Token, cookieOptions);

		if (result.Success)
			return Ok();

		AddErrors(result);
		return BadRequest(ModelState);
	}

	[HttpPut("password-change"), Authorize]
	public async Task<IActionResult> PasswordChange([FromBody] PasswordChangeRequest request)
	{
		if (!ModelState.IsValid)
			return BadRequest(ModelState);


		if (UserHelper.GetUserId(HttpContext, out var userId, out var unauthorized))
			return unauthorized;

		var result = await _userService.ChangePasswordAsync(userId, request);

		if (result.Succeeded)
			return Ok();

		return BadRequest(result.Errors);
	}

	[HttpGet("validate"), Authorize]
	public IActionResult Validate()
	{

		if (User.Identity?.IsAuthenticated ?? false)
			return Ok(new { message = "Token is valid." });


		return Unauthorized(new { message = "Token is invalid or expired." });
	}

	[HttpPost("logout")]
	public IActionResult Logout()
	{
		Response.Cookies.Delete("AuthToken");
		return Ok("Logged out successfully");
	}

	private void AddErrors(AuthResult result)
	{
		foreach (var error in result.ErrorMessages)
			ModelState.AddModelError(error.Key, error.Value);
	}
}

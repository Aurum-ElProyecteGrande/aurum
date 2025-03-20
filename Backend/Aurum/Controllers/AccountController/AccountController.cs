using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;
using Aurum.Services.AccountService;
using Aurum.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.AccountController
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
		private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
			_logger = logger;
		}

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized)) 
                    return unauthorized;
                                
                var accounts = await _accountService.GetAll(userId);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while getting accounts: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
		}

        [HttpPost]
        public async Task<IActionResult> Create(ModifyAccountDto account)
        {
            try
            {
                if (UserHelper.GetUserId(HttpContext,out var userId, out var unauthorized)) 
                    return unauthorized;

                
                var accountId = await _accountService.Create(account, userId);
                return Ok(accountId);
            }
            catch (Exception ex)
            {
				_logger.LogError($"An error occured while creating account: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpPut("{accountId:int}")]
        public async Task<IActionResult> Update(ModifyAccountDto account, int accountId)
        {
            try
            {
                var updatedId = await _accountService.Update(account, accountId);
                return Ok(updatedId);
            }
            catch (Exception ex)
			{
				_logger.LogError($"An error occured while updating account: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }

        [HttpDelete("{accountId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int accountId)
        {
            try
            {
                var isDeleted = await _accountService.Delete(accountId);
                return Ok(isDeleted);
            }
            catch (Exception ex)
			{
				_logger.LogError($"An error occured while deleting account: {ex.Message}");
				return StatusCode(500, "Uh-oh, the gold slipped out of our grasp! Please try again later.");
			}
        }
    }
}

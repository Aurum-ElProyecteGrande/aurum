using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;
using Aurum.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.AccountController
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll([FromRoute] string userId)
        {
            try
            {
                var accounts = await _accountService.GetAll(userId);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModifyAccountDto account)
        {
            try
            {
                var accountId = await _accountService.Create(account);
                return Ok(accountId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{accountId}")]
        public async Task<IActionResult> Update(ModifyAccountDto account, int accountId)
        {
            try
            {
                var updatedId = await _accountService.Update(account, accountId);
                return Ok(updatedId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
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
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

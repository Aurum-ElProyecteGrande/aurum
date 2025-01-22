using Microsoft.AspNetCore.Mvc;
using System;
using Aurum.Models.AccountDto;
using Aurum.Repositories.AccountRepository;

namespace Aurum.Controllers.Account
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;

        public AccountController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> GetAll([FromRoute]int userId)
        {
            try
            {
                var accounts = await _accountRepo.GetAll(userId);
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
                var accountId = await _accountRepo.Create(account);

                if (accountId == 0)
                    throw new InvalidOperationException("Failed to create account. Invalid input.");

                return Ok(accountId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{accountId:int}")]
        public async Task<IActionResult> Update([FromRoute]int accountId, ModifyAccountDto account)
        {
            try
            {
                var updatedId = await _accountRepo.Update(accountId, account);

                return Ok($"Account with ID {updatedId} was successfully updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{accountId:int}")]
        public async Task<IActionResult> Delete([FromRoute]int accountId)
        {
            try
            {
                var isDeleted = await _accountRepo.Delete(accountId);

                if (!isDeleted)
                    throw new InvalidOperationException($"Failed to delete account with ID {accountId}.");

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

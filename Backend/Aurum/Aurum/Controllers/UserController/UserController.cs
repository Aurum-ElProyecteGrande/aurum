using Aurum.Models.UserDto;
using Aurum.Repositories.UserRepository;
using Microsoft.AspNetCore.Mvc;

namespace Aurum.Controllers.UserController;

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> Get([FromRoute] int userId)
        {
            try
            {
                var user = await _userRepo.Get(userId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModifyUserDto user)
        {
            try
            {
                var userId = await _userRepo.Create(user);
                
                return Ok(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId:int}")]
        public async Task<IActionResult> Update([FromRoute] int userId, ModifyUserDto user)
        {
            try
            {
                var updatedId = await _userRepo.Update(userId, user);

                return Ok($"Account with ID {updatedId} was successfully updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId:int}")]
        public async Task<IActionResult> Delete([FromRoute]int userId)
        {
            try
            {
                var isDeleted = await _userRepo.Delete(userId);

                if (!isDeleted)
                    throw new InvalidOperationException($"Failed to delete account with ID {userId}.");

                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
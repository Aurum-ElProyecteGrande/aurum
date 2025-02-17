using Aurum.Data.Entities;
using Aurum.Repositories.UserRepository;

namespace Aurum.Services.UserServices
{
    public class UserService : IUserService
    {
        private IUserRepo _userRepo;

        public UserService(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<User> Get(int userId)
        {
            if (userId == 0) throw new NullReferenceException("User ID can not be 0");

            var user = await _userRepo.Get(userId);

            if (user == null) throw new ArgumentException($"Could not find user with id: {userId}");

            return user;
        }
        public async Task<int> Create(User user)
        {
            var userId = await _userRepo.Create(user);

            if (userId == 0) throw new Exception("Error while creating user");

            return userId;
        }
        public async Task<int> Update(User user)
        {
            var userId = await _userRepo.Update(user);

            if (userId == 0) throw new Exception("Error while updating user");

            return userId;

        }
        public async Task<bool> Delete(int userId)
        {
            var isDeleted = await _userRepo.Delete(userId);

            if (!isDeleted) throw new InvalidOperationException($"Failed to delete account with ID {userId}.");

            return isDeleted;
        }
    }
}

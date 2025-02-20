using Aurum.Data.Context;
using Aurum.Data.Entities;

namespace Aurum.Repositories.UserRepository;

public class UserRepo : IUserRepo
{
    private AurumContext _dbContext;

    public UserRepo(AurumContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> Get(int userId) =>  throw new NotImplementedException();
        /*_dbContext.Users
        .FirstOrDefault(u => u.UserId == userId);*/


    public async Task<int> Create(User user)
    {
        /*await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user.UserId;*/
        throw new NotImplementedException();
    }

    public async Task<int> Update(User user)
    {
        /*_dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return user.UserId;*/
        
        throw new NotImplementedException();
    }

    public async Task<bool> Delete(int userId)
    {
        /*var userToDelete = _dbContext.Users.FirstOrDefault(u => u.UserId == userId);

        if (userToDelete is not null)
        {
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        return false;*/

        throw new NotImplementedException();

    }
}
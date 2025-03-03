/*using Aurum.Data.Entities;
using Aurum.Repositories.UserRepository;
using Aurum.Services.UserServices;
using Moq;

namespace AurumTest.Services.UserServices;

[TestFixture]
[TestOf(typeof(UserService))]
public class UserServiceTest
{
    private Mock<IUserRepo> _userRepoMock;
    private UserService _userService;

    [SetUp]
    public void SetUp()
    {
        _userRepoMock = new Mock<IUserRepo>();
        _userService = new UserService(_userRepoMock.Object);
    }

    [Test]
    public async Task ExistingUserId_ReturnsUser()
    {
        int userId = 1;
        User user = new User { UserId = userId };

        _userRepoMock.Setup(repo => repo.Get(userId)).ReturnsAsync(user);

        var result = await _userService.Get(userId);
        
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.UserId, Is.EqualTo(userId));
        });
    }

    [Test]
    public void ZeroAccountId_ThrowsNullReferenceException()
    {
        Assert.ThrowsAsync<NullReferenceException>(async () => await _userService.Get(0));
    }

    [Test]
    public void NullAccount_ThrowsArgumentException()
    {
        int userId = 1;
        
        _userRepoMock.Setup(repo => repo.Get(userId)).ReturnsAsync((User)null);
        
        Assert.ThrowsAsync<ArgumentException>(async () => await _userService.Get(userId));
    }

    [Test]
    public async Task ValidUser_CreatesUserAndReturnsId()
    {
        int userId = 1;
        User user = new User { UserId = userId };

        _userRepoMock.Setup(repo => repo.Create(user)).ReturnsAsync(userId);

        var result = await _userService.Create(user);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(userId));
            _userRepoMock.Verify(repo => repo.Create(user), Times.Once);
        });
    }

    [Test]
    public void ZeroUserId_ThrowsException_OnCreate()
    {
        int userId = 1;
        User user = new User { UserId = userId };

        _userRepoMock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync(0);

        Assert.Multiple(() =>
        {
            Assert.ThrowsAsync<Exception>(async () => await _userService.Create(user));
            _userRepoMock.Verify(repo => repo.Create(user), Times.Once);
        });
    }
    
    [Test]
    public async Task ValidUser_PassedToRepositoryCorrectly()
    {
        User user = new User { UserId = 1, Username = "John Doe" };

        _userRepoMock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync(1);

        await _userService.Create(user);

        _userRepoMock.Verify(repo => repo.Create(It.Is<User>(u => u.Username == "John Doe")), Times.Once);
    }

    [Test]
    public async Task ValidUser_UpdatesUserAndReturnsId()
    {
        int userId = 1;
        User user = new User{UserId = userId};

        _userRepoMock.Setup(repo => repo.Update(user)).ReturnsAsync(userId);

        var result = await _userService.Update(user);
        
        Assert.Multiple(() =>
        {
            _userRepoMock.Verify(repo => repo.Update(user), Times.Once);
            Assert.That(result, Is.EqualTo(userId));
        });
    }

    [Test]
    public async Task ZeroUserId_ThrowsException_OnUpdate()
    {
        int userId = 0;
        User user = new User { UserId = userId };
        
        _userRepoMock.Setup(repo => repo.Update(user)).ReturnsAsync(0);
        
        Assert.ThrowsAsync<Exception>(async () => await _userService.Update(user));
    }

    [Test]
    public async Task ValidUserId_DeletesUserAndReturnsTrue()
    {
        int userId = 1;
        User user = new User { UserId = 1 };

        _userRepoMock.Setup(repo => repo.Delete(userId)).ReturnsAsync(true);

        var result = await _userService.Delete(userId);
        
        Assert.Multiple(() =>
        {
            Assert.IsTrue(result);
            _userRepoMock.Verify(repo => repo.Delete(userId), Times.Once);
        });
    }

    [Test]
    public async Task InvalidUserId_ThrowsInvalidOperationException()
    {
        int userId = 99;

        _userRepoMock.Setup(repo => repo.Delete(userId)).ReturnsAsync(false);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _userService.Delete(userId));
    }
    
}*/
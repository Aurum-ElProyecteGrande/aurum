using Aurum.Data.Entities;
using Aurum.Repositories.AccountRepository;
using Aurum.Services.AccountService;
using Aurum.Models.AccountDto;
using Moq;

namespace AurumTest.AccountTests.AccountServiceTest;

[TestFixture]
[TestOf(typeof(Aurum.Services.AccountService.AccountService))]
public class AccountServiceTest
{
    private Mock<IAccountRepo> _accountRepoMock;
    private AccountService _accountService;

    [SetUp]
    public void Setup()
    {
        _accountRepoMock = new Mock<IAccountRepo>();
        _accountService = new AccountService(_accountRepoMock.Object);
    }

    [Test]
    public async Task ExistingAccountId_ReturnsAccount()
    {
        int accountId = 1;
        Account account = new Account { AccountId = accountId };

        _accountRepoMock.Setup(repo => repo.Get(accountId)).ReturnsAsync(account);

        var result = await _accountService.Get(accountId);

        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(result.AccountId, Is.EqualTo(accountId));
            _accountRepoMock.Verify(repo => repo.Get(It.IsAny<int>()), Times.Once);
        });
    }

    [Test]
    public async Task NonExistingAccountId_ReturnsNull()
    {
        int accountId = 99;

        _accountRepoMock.Setup(repo => repo.Get(accountId)).ReturnsAsync((Account)null);

        var result = await _accountService.Get(accountId);
        
        Assert.Multiple(() =>
        {
            Assert.IsNull(result);
            _accountRepoMock.Verify(repo => repo.Get(accountId), Times.Once);
        });
    }

    [Test]
    public void ZeroAccountId_ThrowsArgumentNullException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _accountService.Get(0));
    }

    [Test]
    public async Task ValidAccountId_ReturnsInitialAmount()
    {
        int accountId = 1;
        decimal accountAmount = 99.90m;
        Account account = new Account { AccountId = accountId, Amount = accountAmount };

        _accountRepoMock.Setup(repo => repo.Get(accountId)).ReturnsAsync(account);

        var result = await _accountService.GetInitialAmount(accountId);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(accountAmount));
            _accountRepoMock.Verify(repo => repo.Get(accountId), Times.Once);
        });
    }

    [Test]
    public async Task ZeroAccountId_ThrowsException_InGetInitialAmount()
    {
        int accountId = 0;
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _accountService.GetInitialAmount(accountId));
    }

    [Test]
    public async Task ValidUserId_ReturnsListOfAccounts()
    {
        int userId = 10;
        Account account1 = new Account { AccountId = 1, Amount = 100 };
        Account account2 = new Account { AccountId = 2, Amount = 200 };
        var accounts = new List<Account> { account1, account2 };

        _accountRepoMock.Setup(repo => repo.GetAll(userId)).ReturnsAsync(accounts);

        var result = await _accountService.GetAll(userId);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EquivalentTo(accounts));
            _accountRepoMock.Verify(repo => repo.GetAll(userId), Times.Once);
        });
    }

    [Test]
    public void ZeroUserId_ThrowsArgumentNullException()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _accountService.GetAll(0));
    }

    [Test]
    public async Task ValidAccount_CreatesAndReturnsId()
    {
        int accountId = 1;
        Account account = new Account { AccountId = accountId };

        _accountRepoMock.Setup(repo => repo.Create(account)).ReturnsAsync(accountId);

        var result = await _accountService.Create(account);
        
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.EqualTo(accountId));
            _accountRepoMock.Verify(repo => repo.Create(account), Times.Once);
        });
    }

    [Test]
    public void InvalidAccount_ThrowsInvalidOperationException()
    {
        Account account = new Account { AccountId = 1 };

        _accountRepoMock.Setup(repo => repo.Create(account)).ReturnsAsync(0);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _accountService.Create(account));
    }

    [Test]
    public async Task ValidAccount_UpdatedAndReturnsId()
    {
        int accountId = 1;
        Account account = new Account { AccountId = accountId };

        _accountRepoMock.Setup(repo => repo.Update(account)).ReturnsAsync(accountId);

        var result = await _accountService.Update(account);
        
        Assert.Multiple(() =>
        {
            _accountRepoMock.Verify(repo => repo.Update(It.IsAny<Account>()), Times.Once);
            Assert.That(result, Is.EqualTo(accountId));
        });
    }

    [Test]
    public void InvalidAccount_ThrowsInvalidOperationException_OnUpdate()
    {
        Account account = new Account { AccountId = 1 };

        _accountRepoMock.Setup(repo => repo.Update(account)).ReturnsAsync(0);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _accountService.Update(account));
    }

    [Test]
    public async Task ExistingAccountId_DeletesSuccessfully()
    {
        int accountId = 1;

        _accountRepoMock.Setup(repo => repo.Delete(accountId)).ReturnsAsync(true);

        var result = await _accountService.Delete(accountId);

        Assert.Multiple(() =>
        {
            Assert.IsTrue(result);
            _accountRepoMock.Verify(repo => repo.Delete(accountId), Times.Once);
        });
    }

    [Test]
    public void NonExistingAccountId_ThrowsInvalidOperationException()
    {
        int accountId = 99;

        _accountRepoMock.Setup(repo => repo.Delete(accountId)).ReturnsAsync(false);

        Assert.ThrowsAsync<InvalidOperationException>(async () => await _accountService.Delete(accountId));
    }
}
using Aurum.Controllers.ExpenseController;
using Aurum.Models.CategoryDto;
using Aurum.Models.ExpenseDto;
using Aurum.Models.ExpenseDtos;
using Aurum.Services.ExpenseService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AurumTest.ExpenseTests;

public class ExpenseControllerTest
{
	 private Mock<IExpenseService> _serviceMock;
    private ExpenseController _controller;

    [SetUp]
    public void SetUp()
    {
        _serviceMock = new Mock<IExpenseService>();
        _controller = new ExpenseController(_serviceMock.Object);
    }

    [Test]
    public async Task GetAll_ExpensesExists_ShouldReturnOk()
    {
        // Arrange
        var expenses = new List<ExpenseDto>
        {
            new ExpenseDto(new CategoryDto("Test Category", 1), null, "Test Expense", 100, DateTime.Now)
        };

        _serviceMock
            .Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(expenses);

        // Act
        var result = await _controller.GetAll(1, 1);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(expenses));
        });
    }

    [Test]
    public async Task GetAll_ExceptionThrows_ShouldReturn500()
    {
        // Arrange
        _serviceMock.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.GetAll(1, 1);

        // Assert
        var statusCodeResult = result as ObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
            Assert.That(statusCodeResult.Value, Is.EqualTo("Test exception"));
        });
    }

    [Test]
    public async Task GetAll_WithDates_ExpensesExist_ShouldReturnOk()
    {
        // Arrange
        var expenses = new List<ExpenseDto>
        {
            new ExpenseDto(new CategoryDto("Test Category", 1), null, "Test Expense", 100, DateTime.Now)
        };

        _serviceMock.Setup(s => s.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(expenses);

        // Act
        var result = await _controller.GetAll(1, 1, DateTime.Now.AddDays(-1), DateTime.Now);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(expenses));
        });
    }

    [Test]
    public async Task Create_ExpenseCreated_ShouldReturnOk()
    {
        // Arrange
        var expense = new ModifyExpenseDto(1, 1, "SubCategory1", "Test Expense", 100, DateTime.Now);
        _serviceMock
            .Setup(s => s.Create(It.IsAny<ModifyExpenseDto>()))
            .ReturnsAsync(1);

        // Act
        var result = await _controller.Create(expense);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(1));
        });
    }

    [Test]
    public async Task Create_ExceptionThrown_ShouldReturn500()
    {
        // Arrange
        var expense = new ModifyExpenseDto(1, 1, "SubCategory1", "Test Expense", 100, DateTime.Now);
        _serviceMock
            .Setup(s => s.Create(It.IsAny<ModifyExpenseDto>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Create(expense);

        // Assert
        var statusCodeResult = result as ObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
            Assert.That(statusCodeResult.Value, Is.EqualTo("Test exception"));
        });
    }

    [Test]
    public async Task Delete_Success_ShouldReturnOk()
    {
        // Arrange
        _serviceMock
            .Setup(s => s.Delete(It.IsAny<int>()))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
            Assert.That(okResult.Value, Is.EqualTo(true));
        });
    }

    [Test]
    public async Task Delete_ExceptionThrown_ShouldReturn500()
    {
        // Arrange
        _serviceMock.Setup(s => s.Delete(It.IsAny<int>()))
            .ThrowsAsync(new Exception("Test exception"));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var statusCodeResult = result as ObjectResult;
        Assert.Multiple(() =>
        {
            Assert.That(statusCodeResult, Is.Not.Null);
            Assert.That(statusCodeResult.StatusCode, Is.EqualTo(500));
            Assert.That(statusCodeResult.Value, Is.EqualTo("Test exception"));
        });
    }
}
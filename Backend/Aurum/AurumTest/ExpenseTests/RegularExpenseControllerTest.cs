using Aurum.Controllers.RegularExpressController;
using Aurum.Models.CategoryDto;
using Aurum.Models.Regular_enum;
using Aurum.Models.RegularExpenseDto;
using Aurum.Services.RegularExpenseService;
using Microsoft.AspNetCore.Mvc;
using Moq;
using static NUnit.Framework.Assert;

namespace AurumTest.ExpenseTests;

public class RegularExpenseControllerTest
{
    private Mock<IRegularExpenseService> _serviceMock;
    private RegularExpenseController _controller;

    [SetUp]
    public void SetUp()
    {
        _serviceMock = new Mock<IRegularExpenseService>();
        _controller = new RegularExpenseController(_serviceMock.Object);
    }

    [Test]
    public async Task GetAll_ValidExpenses_ReturnsOkResult()
    {
        // Arrange
        var accountId = 1;
        var userId = 1;
        var regularExpenses = new List<RegularExpenseDto>
        {
            new RegularExpenseDto(1, accountId, new CategoryDto("Category 1", 1), null, "Expense 1", 100, DateTime.Now, Regularity.Daily)
        };
        _serviceMock
            .Setup(s => s.GetAll(accountId, userId))
            .ReturnsAsync(regularExpenses);

        // Act
        var result = await _controller.GetAll(accountId, userId);

        // Assert
        var okResult = result as OkObjectResult;
        That(okResult, Is.Not.Null);
        var returnValue = okResult?.Value as List<RegularExpenseDto>;
        That(returnValue, Is.Not.Null);
        That(returnValue?.Count, Is.EqualTo(regularExpenses.Count));
    }

    [Test]
    public async Task GetAll_ExceptionThrows_ReturnsStatusCode500()
    {
        // Arrange
        var accountId = 1;
        var userId = 1;
        _serviceMock
            .Setup(s => s.GetAll(accountId, userId))
            .ThrowsAsync(new Exception("An error occurred"));

        // Act
        var result = await _controller.GetAll(accountId, userId);

        // Assert
        var statusCodeResult = result as ObjectResult;
        That(statusCodeResult, Is.Not.Null, "An error occurred");
        That(statusCodeResult?.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task Create_Valid_ReturnsOkResult()
    {
        // Arrange
        var modifyExpenseDto = new ModifyRegularExpenseDto(0, 1, 1, null, "Expense 1", 100, DateTime.Now, Regularity.Daily);
        var createdId = 1;
        _serviceMock
            .Setup(s => s.Create(modifyExpenseDto))
            .ReturnsAsync(createdId);

        // Act
        var result = await _controller.Create(modifyExpenseDto);

        // Assert
        var okResult = result as OkObjectResult;
        That(okResult, Is.Not.Null);
        That(okResult?.Value, Is.EqualTo(createdId));
    }

    [Test]
    public async Task Create_ExceptionThrows_ReturnsStatusCode500()
    {
        // Arrange
        var modifyExpenseDto = new ModifyRegularExpenseDto(0, 1, 1, null, "Expense 1", 100, DateTime.Now, Regularity.Daily);
        _serviceMock.Setup(s => s.Create(modifyExpenseDto)).ThrowsAsync(new Exception("An error occurred"));

        // Act
        var result = await _controller.Create(modifyExpenseDto);

        // Assert
        var statusCodeResult = result as ObjectResult;
        That(statusCodeResult, Is.Not.Null, "An error occurred");
        That(statusCodeResult?.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task Update_Valid_ReturnsOkResult()
    {
        // Arrange
        var modifyExpenseDto = new ModifyRegularExpenseDto(1, 1, 1, null, "Updated Expense", 150, DateTime.Now, Regularity.Monthly);
        var updatedId = 1;
        _serviceMock
            .Setup(s => s.Update(modifyExpenseDto))
            .ReturnsAsync(updatedId);

        // Act
        var result = await _controller.Update(modifyExpenseDto);

        // Assert
        var okResult = result as OkObjectResult;
        That(okResult, Is.Not.Null);
        That(okResult?.Value, Is.EqualTo(updatedId));
    }

    [Test]
    public async Task Update_ExceptionThrows_ReturnsStatusCode500()
    {
        // Arrange
        var modifyExpenseDto = new ModifyRegularExpenseDto(1, 1, 1, null, "Updated Expense", 150, DateTime.Now, Regularity.Monthly);
        _serviceMock
            .Setup(s => s.Update(modifyExpenseDto))
            .ThrowsAsync(new Exception("An error occurred"));

        // Act
        var result = await _controller.Update(modifyExpenseDto);

        // Assert
        var statusCodeResult = result as ObjectResult;
        That(statusCodeResult, Is.Not.Null, "An error occurred");
        That(statusCodeResult?.StatusCode, Is.EqualTo(500));
    }

    [Test]
    public async Task Delete_Success_ReturnsOkResult()
    {
        // Arrange
        var regularId = 1;
        _serviceMock
            .Setup(s => s.Delete(regularId))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(regularId);

        // Assert
        var okResult = result as OkObjectResult;
        That(okResult, Is.Not.Null);
        That(okResult?.Value, Is.EqualTo(true));
    }

    [Test]
    public async Task Delete_ExceptionThrows_ReturnsStatusCode500()
    {
        // Arrange
        var regularId = 1;
        _serviceMock
            .Setup(s => s.Delete(regularId))
            .ThrowsAsync(new Exception("An error occurred"));

        // Act
        var result = await _controller.Delete(regularId);

        // Assert
        var statusCodeResult = result as ObjectResult;
        That(statusCodeResult, Is.Not.Null, "An error occurred.");
        That(statusCodeResult?.StatusCode, Is.EqualTo(500));
    }
}
using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.ExpenseDto;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Aurum.Services.ExpenseService;

namespace AurumTest.ExpenseTests;

public class ExpenseServiceTest
{
    private Mock<IExpenseRepository> _repositoryMock;
    private Mock<IExpenseCategoryService> _categoryServiceMock;
    private ExpenseService _expenseService;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IExpenseRepository>();
        _categoryServiceMock = new Mock<IExpenseCategoryService>();
        _expenseService = new ExpenseService(_repositoryMock.Object, _categoryServiceMock.Object);
    }

    [Test]
    public async Task GetAll_NoExpensesFound_ShouldReturnEmptyList()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetAll(It.IsAny<int>()))
            .ReturnsAsync(new List<Expense>());

        // Act
        var result = await _expenseService.GetAll(1);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAll_WithDateRange_NoExpensesFound_ShouldReturnEmptyList()
    {
        // Arrange
        _repositoryMock
            .Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(new List<Expense>());

        // Act
        var result = await _expenseService.GetAll(1, DateTime.Now.AddDays(-7), DateTime.Now);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAll_ValidData_ShouldReturnMappedExpenseDtos()
    {
        // Arrange
        var rawExpenses = new List<Expense>
        {
            new()
            {
                AccountId = 1,
                ExpenseCategoryId = 1,
                ExpenseSubCategoryId = 2,
                Label = "Test Expense",
                Amount = 100,
                Date = DateTime.Now,
                ExpenseCategory = new ExpenseCategory { Name = "Category1" },
                ExpenseSubCategory = new ExpenseSubCategory { Name = "SubCategory1" },
                Account = new Account { Currency = new Currency { Name = "USD", CurrencyCode = "USD", Symbol = "$" } }
            }
        };

        _repositoryMock
            .Setup(r => r.GetAll(It.IsAny<int>()))
            .ReturnsAsync(rawExpenses);

        // Act
        var result = await _expenseService.GetAll(1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Label, Is.EqualTo("Test Expense"));
            Assert.That(result[0].Amount, Is.EqualTo(100));
            Assert.That(result[0].Category.Name, Is.EqualTo("Category1"));
            Assert.That(result[0].Subcategory.Name, Is.EqualTo("SubCategory1"));
            Assert.That(result[0].Currency.CurrencyCode, Is.EqualTo("USD"));
        });
    }

    [Test]
    public async Task Create_ValidData_ShouldCallRepositoryCreate()
    {
        // Arrange
        var expenseDto = new ModifyExpenseDto(1, 1, "SubCategory1", "Test Expense", 100, DateTime.Now);

        _categoryServiceMock.Setup(s => s.AcquireSubCategoryId(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(2);
        _repositoryMock.Setup(r => r.Create(It.IsAny<Expense>()))
            .ReturnsAsync(1);

        // Act
        var result = await _expenseService.Create(expenseDto);

        // Assert
        _categoryServiceMock.Verify(s => s.AcquireSubCategoryId(1, "SubCategory1"), Times.Once);
        _repositoryMock.Verify(r => r.Create(It.IsAny<Expense>()), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public async Task Create_ShouldHandleNullSubCategoryName()
    {
        // Arrange
        var expenseDto = new ModifyExpenseDto(1, 1, null, "Test Expense", 100, DateTime.Now);

        _repositoryMock.Setup(r => r.Create(It.IsAny<Expense>()))
            .ReturnsAsync(1);

        // Act
        var result = await _expenseService.Create(expenseDto);

        // Assert
        _repositoryMock.Verify(r => r.Create(It.Is<Expense>(e =>
            e.ExpenseSubCategoryId == null &&
            e.ExpenseCategoryId == 1 &&
            e.Label == "Test Expense" &&
            e.Amount == 100)), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public async Task Delete_Success_ShouldReturnTrue()
    {
        // Arrange
        _repositoryMock.Setup(r => r.Delete(It.IsAny<int>()))
            .ReturnsAsync(true);

        // Act
        var result = await _expenseService.Delete(1);

        // Assert
        Assert.That(result, Is.True);
        _repositoryMock.Verify(r => r.Delete(1), Times.Once);
    }

    [Test]
    public async Task Delete_Fails_ShouldReturnFalse()
    {
        // Arrange
        _repositoryMock.Setup(r => r.Delete(It.IsAny<int>()))
            .ReturnsAsync(false);

        // Act
        var result = await _expenseService.Delete(1);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task GetTotalExpense_ShouldReturnSumOfAllExpenses()
    {
        // Arrange
        var rawExpenses = new List<Expense>
        {
            new() { Amount = 100 },
            new() { Amount = 200 }
        };

        _repositoryMock.Setup(r => r.GetAll(It.IsAny<int>()))
            .ReturnsAsync(rawExpenses);

        // Act
        var result = await _expenseService.GetTotalExpense(1);

        // Assert
        Assert.That(result, Is.EqualTo(300));
    }

    [Test]
    public async Task GetTotalExpense_WithDate_ShouldReturnSumOfFilteredExpenses()
    {
        // Arrange
        var rawExpenses = new List<Expense>
        {
            new() { Amount = 150 },
            new() { Amount = 350 }
        };

        _repositoryMock.Setup(r => r.GetAll(It.IsAny<int>(), It.IsAny<DateTime>()))
            .ReturnsAsync(rawExpenses);

        // Act
        var result = await _expenseService.GetTotalExpense(1, DateTime.Now);

        // Assert
        Assert.That(result, Is.EqualTo(500));
    }
}

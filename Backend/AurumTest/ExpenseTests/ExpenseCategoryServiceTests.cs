using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Repositories.ExpenseCategoryRepository;
using Aurum.Services.ExpenseCategoryService;
using Moq;
using NUnit.Framework;

namespace AurumTest.ExpenseTests;

[TestFixture]
public class ExpenseCategoryServiceTests
{
    private Mock<IExpenseCategoryRepository> _repositoryMock;
    private ExpenseCategoryService _service;

    [SetUp]
    public void Setup()
    {
        _repositoryMock = new Mock<IExpenseCategoryRepository>();
        _service = new ExpenseCategoryService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllExpenseCategories_ValidData_ShouldReturnDictionary()
    {
        // Arrange
        var userId = "user1";
        var categories = new List<ExpenseCategory>
        {
            new() { Name = "Category1", ExpenseCategoryId = 1 },
            new() { Name = "Category2", ExpenseCategoryId = 2 }
        };

        var subCategories = new List<ExpenseSubCategory>
        {
            new() { Name = "SubCategory1", ExpenseCategoryId = 1, ExpenseSubCategoryId = 1 },
            new() { Name = "SubCategory2", ExpenseCategoryId = 2, ExpenseSubCategoryId = 2 }
        };

        _repositoryMock
            .Setup(r => r.GetAllCategory())
            .ReturnsAsync(categories);
        _repositoryMock
            .Setup(r => r.GetAllSubCategory(userId))
            .ReturnsAsync(subCategories);

        // Act
        var result = await _service.GetAllExpenseCategories(userId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result.Keys, Has.Exactly(1).Matches<CategoryDto>(c => c.Name == "Category1" && c.CategoryId == 1));
            Assert.That(result.Keys, Has.Exactly(1).Matches<CategoryDto>(c => c.Name == "Category2" && c.CategoryId == 2));
            Assert.That(result.Values, Has.Exactly(1).Matches<List<SubCategoryDto>>(s => s.Any(sub => sub.Name == "SubCategory1")));
            Assert.That(result.Values, Has.Exactly(1).Matches<List<SubCategoryDto>>(s => s.Any(sub => sub.Name == "SubCategory2")));
        });
    }

    [Test]
    public void GetAllExpenseCategories_EmptyData_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var userId = "user1";
        _repositoryMock
            .Setup(r => r.GetAllCategory())
            .ReturnsAsync(new List<ExpenseCategory>());
        _repositoryMock
            .Setup(r => r.GetAllSubCategory(userId))
            .ReturnsAsync(new List<ExpenseSubCategory>());

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetAllExpenseCategories(userId));
    }

    [Test]
    public async Task AcquireSubCategoryId_ExistingSubCategory_ShouldReturnId()
    {
        // Arrange
        var categoryId = 1;
        var subCategoryName = "SubCategory1";
        var expectedId = 1;

        _repositoryMock
            .Setup(r => r.GetSubCategoryByName(categoryId, subCategoryName))
            .ReturnsAsync(expectedId);

        // Act
        var result = await _service.AcquireSubCategoryId(categoryId, subCategoryName);

        // Assert
        Assert.That(result, Is.EqualTo(expectedId));
    }

    [Test]
    public async Task AcquireSubCategoryId_NonExistentSubCategory_ShouldCreateAndReturnId()
    {
        // Arrange
        var categoryId = 1;
        var subCategoryName = "NewSubCategory";
        var createdId = 2;

        _repositoryMock
            .Setup(r => r.GetSubCategoryByName(categoryId, subCategoryName))
            .ReturnsAsync((int?)null);
        _repositoryMock
            .Setup(r => r.CreateSubCategory(categoryId, subCategoryName))
            .ReturnsAsync(createdId);

        // Act
        var result = await _service.AcquireSubCategoryId(categoryId, subCategoryName);

        // Assert
        Assert.That(result, Is.EqualTo(createdId));
    }
}

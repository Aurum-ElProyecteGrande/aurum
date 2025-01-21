using Aurum.Models.CategoryDto;
using Aurum.Models.ExpenseDto;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;
using Moq;

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
            .ReturnsAsync(new List<RawExpenseDto>());

        // Act
        var result = await _expenseService.GetAll(1, 1);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAll_NoCategoriesFound_ShouldThrowException()
    {
        // Arrange
        var rawExpenses = new List<RawExpenseDto>
        {
            new(1, 2, "Test Expense", 100, DateTime.Now)
        };

        _repositoryMock
            .Setup(r => r.GetAll(It.IsAny<int>()))
            .ReturnsAsync(rawExpenses);
        _categoryServiceMock
            .Setup(s => s.GetAllExpenseCategories(It.IsAny<int>()))
            .ReturnsAsync(new Dictionary<CategoryDto, List<SubCategoryDto>>());

        // Act & Assert
        Assert.ThrowsAsync<InvalidDataException>(() => _expenseService.GetAll(1, 1));
    }

    [Test]
    public async Task GetAll_ValidData_ShouldReturnMappedExpenseDtos()
    {
        // Arrange
        var rawExpenses = new List<RawExpenseDto>
        {
            new(1, 2, "Test Expense", 100, DateTime.Now)
        };

        var categories = new Dictionary<CategoryDto, List<SubCategoryDto>>
        {
            [new CategoryDto("Category1", 1)] = new List<SubCategoryDto>
            {
                new SubCategoryDto("SubCategory1", 2, 1)
            }
        };

        _repositoryMock
            .Setup(r => r.GetAll(It.IsAny<int>()))
            .ReturnsAsync(rawExpenses);
        _categoryServiceMock
            .Setup(s => s.GetAllExpenseCategories(It.IsAny<int>()))
            .ReturnsAsync(categories);

        // Act
        var result = await _expenseService.GetAll(1, 1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].Label, Is.EqualTo("Test Expense"));
            Assert.That(result[0].Amount, Is.EqualTo(100));
            Assert.That(result[0].Category.Name, Is.EqualTo("Category1"));
            Assert.That(result[0].Subcategory.Name, Is.EqualTo("SubCategory1"));
        });
    }

    [Test]
    public async Task Create_ValidData_ShouldCallRepositoryCreate()
    {
        // Arrange
        var expenseDto = new ModifyExpenseDto(1, 1, "SubCategory1", "Test Expense", 100, DateTime.Now);

        _categoryServiceMock.Setup(s => s.AcquireSubCategoryId(It.IsAny<int>(), It.IsAny<string>()))
            .ReturnsAsync(2);
        _repositoryMock.Setup(r => r.Create(It.IsAny<RawExpenseDto>()))
            .ReturnsAsync(1);

        // Act
        var result = await _expenseService.Create(expenseDto);

        // Assert
        _categoryServiceMock.Verify(s => s.AcquireSubCategoryId(1, "SubCategory1"), Times.Once);
        _repositoryMock.Verify(r => r.Create(It.IsAny<RawExpenseDto>()), Times.Once);
        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public async Task Create_ShouldHandleNullSubCategoryName()
    {
        // Arrange
        var expenseDto = new ModifyExpenseDto(1, 1, null, "Test Expense", 100, DateTime.Now);

        _repositoryMock.Setup(r => r.Create(It.IsAny<RawExpenseDto>()))
            .ReturnsAsync(1);

        // Act
        var result = await _expenseService.Create(expenseDto);

        // Assert
        _repositoryMock.Verify(r => r.Create(It.Is<RawExpenseDto>(e =>
            e.SubCategoryId == null &&
            e.CategoryId == 1 &&
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
}
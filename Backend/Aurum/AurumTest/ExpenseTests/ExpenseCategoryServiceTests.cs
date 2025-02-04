using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Repositories.ExpenseCategoryRepository;
using Aurum.Services.ExpenseCategoryService;
using Moq;

namespace AurumTest.ExpenseTests;

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
		var userId = 1;
		var categories = new List<ExpenseCategory>
		{
			new ()
			{
				Name = "Category1",
				ExpenseCategoryId = 1,
			},new ()
			{
				Name = "Category2",
				ExpenseCategoryId = 2,
			}
		};
		var subCategories = new List<ExpenseSubCategory>
		{
			new ()
			{
				Name = "Category1",
				ExpenseCategoryId = 1, 
				ExpenseSubCategoryId = 1
			},
			new ()
			{
				Name = "Category2",
				ExpenseCategoryId = 2, 
				ExpenseSubCategoryId = 2
			}
		};
		
		var resultCategories = new List<CategoryDto>
		{
			new CategoryDto("Category1", 1),
			new CategoryDto("Category2", 2)
		};
		var resultSubCategories = new List<SubCategoryDto>
		{
			new SubCategoryDto ("Category1", 1, 1),
			new SubCategoryDto("Category2", 2, 2)
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
            Assert.That(result.ContainsKey(resultCategories[0]), Is.True);
            Assert.That(result.ContainsKey(resultCategories[1]), Is.True);
        });
    }
	
	[Test]
	public void GetAllExpenseCategories_InvalidEmptyLists_ShouldThrowInvalidOperationException()
	{
		// Arrange
		var userId = 1;
		_repositoryMock
			.Setup(r => r.GetAllCategory())
			.ReturnsAsync([]);
		_repositoryMock
			.Setup(r => r.GetAllSubCategory(userId))
			.ReturnsAsync([]);

		// Act & Assert
		Assert.ThrowsAsync<InvalidOperationException>(async () => await _service.GetAllExpenseCategories(userId));
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
	public async Task AcquireSubCategoryId_NotExistingSubCategory_ShouldCreateSubCategory()
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
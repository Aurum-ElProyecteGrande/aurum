using Aurum.Controllers.ExpenseCategoryController;
using Aurum.Models.CategoryDto;
using Aurum.Services.ExpenseCategoryService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AurumTest.ExpenseTests;

public class ExpenseCategoryControllerTest
{
	private Mock<IExpenseCategoryService> _serviceMock;
	private ExpenseCategoryController _controller;

	[SetUp]
	public void Setup()
	{
		_serviceMock = new Mock<IExpenseCategoryService>();
		_controller = new ExpenseCategoryController(_serviceMock.Object);
	}
	
	[Test]
	public async Task GetAll_CategoriesFound_ShouldReturnOk()
	{
		// Arrange
		var userId = 1;
		var categories = new Dictionary<CategoryDto, List<SubCategoryDto>>
		{
			{ new CategoryDto("Category1", 1), new List<SubCategoryDto>
				{
					new SubCategoryDto("SubCategory1", 1,1)
				}
			}
		};

		_serviceMock
			.Setup(s => s.GetAllExpenseCategories(userId))
			.ReturnsAsync(categories);

		// Act
		var result = await _controller.GetAll(userId) as OkObjectResult;

		// Assert
        Assert.Multiple(() =>
        {
			Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(categories));
        });
    }
	
	[Test]
	public async Task GetAll_NoCategoriesFound_ShouldReturn500()
	{
		// Arrange
		var userId = 1;

		_serviceMock
			.Setup(s => s.GetAllExpenseCategories(userId))
			.ReturnsAsync(new Dictionary<CategoryDto, List<SubCategoryDto>>());

		// Act
		var result = await _controller.GetAll(userId) as ObjectResult;

		// Assert
        Assert.Multiple(() =>
        {
			Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(500));
            Assert.That(result.Value, Is.EqualTo("No expense categories found"));
        });
    }
	
	[Test]
	public async Task GetAll_ServiceThrowsError_ShouldReturn500()
	{
		// Arrange
		var userId = 1;
		var exceptionMessage = "Unexpected error";

		_serviceMock
			.Setup(s => s.GetAllExpenseCategories(userId))
			.ThrowsAsync(new Exception(exceptionMessage));

		// Act
		var result = await _controller.GetAll(userId) as ObjectResult;

		// Assert
        Assert.Multiple(() =>
        {
			Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(500));
            Assert.That(result.Value, Is.EqualTo(exceptionMessage));
        });
    }



}
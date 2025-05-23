using Aurum.Controllers.RegularExpressController;
using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.RegularityEnum;
using Aurum.Models.RegularExpenseDto;
using Aurum.Repositories.RegularExpenseRepository;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.RegularExpenseService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AurumTest.ExpenseTests
{
    public class RegularExpenseServiceTest
    {
        private Mock<IRegularExpenseRepository> _repositoryMock;
        private Mock<IExpenseCategoryService> _categoryServiceMock;
        private RegularExpenseService _service;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRegularExpenseRepository>();
            _categoryServiceMock = new Mock<IExpenseCategoryService>();
            _service = new RegularExpenseService(_repositoryMock.Object, _categoryServiceMock.Object);
        }

        [Test]
        public async Task GetAll_ExpensesExist_ReturnsListOfRegularExpenseDtos()
        {
            // Arrange
            var accountId = 1;
            var userId = "1"; // Updated to string to match method signature
            var rawExpenses = new List<RegularExpense>
            {
                new RegularExpense
                {
                    RegularExpenseId = 1,
                    AccountId = accountId,
                    ExpenseCategoryId = 1,
                    ExpenseSubCategory = null,
                    Label = "Expense 1",
                    Amount = 100,
                    StartDate = DateTime.Now,
                    Regularity = Regularity.Monthly
                }
            };
            var categories = new Dictionary<CategoryDto, List<SubCategoryDto>>
            {
                { new CategoryDto("Category 1", 1), new List<SubCategoryDto>() }
            };

            _repositoryMock
                .Setup(r => r.GetAllRegular())
                .ReturnsAsync(rawExpenses);
            _categoryServiceMock
                .Setup(c => c.GetAllExpenseCategories(userId))
                .ReturnsAsync(categories);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(1));
                Assert.That(result[0].Label, Is.EqualTo("Expense 1"));
                Assert.That(result[0].Amount, Is.EqualTo(100));
            });
        }

        [Test]
        public async Task Create_Valid_ReturnsId()
        {
            // Arrange
            var expenseDto = new ModifyRegularExpenseDto(1, 1, null, "Expense 1", 100, DateTime.Now, Regularity.Monthly);
            var createdId = 1;
            var regularId = 0;
            _categoryServiceMock
                .Setup(c => c.AcquireSubCategoryId(expenseDto.CategoryId, expenseDto.SubCategoryName))
                .ReturnsAsync(1);
            _repositoryMock
                .Setup(r => r.Create(It.IsAny<RegularExpense>()))
                .ReturnsAsync(createdId);

            // Act
            var result = await _service.Create(expenseDto);

            // Assert
            Assert.That(result, Is.EqualTo(createdId));
        }

        [Test]
        public async Task Create_NoSubCategory_ThrowsException()
        {
            // Arrange
            var expenseDto = new ModifyRegularExpenseDto( 1, 1, "NonExistingSubCategory", "Expense 1", 100, DateTime.Now, Regularity.Monthly);
            _categoryServiceMock
                .Setup(c => c.AcquireSubCategoryId(expenseDto.CategoryId, expenseDto.SubCategoryName))
                .ThrowsAsync(new KeyNotFoundException("SubCategory not found"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<KeyNotFoundException>(() => _service.Create(expenseDto));
            Assert.That(ex.Message, Is.EqualTo("SubCategory not found"));
        }

        [Test]
        public async Task Update_Valid_ReturnsUpdatedId()
        {
            // Arrange
            var expenseDto = new ModifyRegularExpenseDto(1, 1, null, "Updated Expense", 150, DateTime.Now, Regularity.Monthly);
            var updatedId = 1;
            var regularId = 0;
            _categoryServiceMock
                .Setup(c => c.AcquireSubCategoryId(expenseDto.CategoryId, expenseDto.SubCategoryName))
                .ReturnsAsync(1);
            _repositoryMock
                .Setup(r => r.Update(It.IsAny<RegularExpense>()))
                .ReturnsAsync(updatedId);

            // Act
            var result = await _service.Update(regularId, expenseDto);

            // Assert
            Assert.That(result, Is.EqualTo(updatedId));
        }

        [Test]
        public async Task Delete_Valid_ReturnsTrue()
        {
            // Arrange
            var expenseId = 1;
            _repositoryMock
                .Setup(r => r.Delete(expenseId))
                .ReturnsAsync(true);

            // Act
            var result = await _service.Delete(expenseId);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task Delete_Fails_ReturnsFalse()
        {
            // Arrange
            var expenseId = 1;
            _repositoryMock
                .Setup(r => r.Delete(expenseId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.Delete(expenseId);

            // Assert
            Assert.That(result, Is.False);
        }
    }
}

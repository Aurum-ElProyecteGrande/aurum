using Aurum.Data.Entities;
using Aurum.Repositories.IncomeRepository.IncomeCategoryRepository;
using Aurum.Services.IncomeCategoryServices;
using Moq;
using NUnit.Framework.Internal;

namespace AurumTest.Services.IncomeCategoryServices;

[TestFixture]
[TestOf(typeof(IncomeCategoryService))]
public class IncomeCategoryServiceTest
{
    private Mock<IIncomeCategoryRepo> _mockIncomeCategoryRepo;
    private IIncomeCategoryService _incomeCategoryService;

    [SetUp]
    public void SetUp()
    {
        _mockIncomeCategoryRepo = new Mock<IIncomeCategoryRepo>();
        _incomeCategoryService = new IncomeCategoryService(_mockIncomeCategoryRepo.Object);
    }

    [Test]
    public async Task GetAll_Returns_ListOfIncomeCategories()
    {
        List<IncomeCategory> mockCategories = new List<IncomeCategory>
        {
            new IncomeCategory { IncomeCategoryId = 1, Name = "Salary"},
            new IncomeCategory { IncomeCategoryId = 2, Name = "Freelance"}
        };

        _mockIncomeCategoryRepo.Setup(repo => repo.GetAllCategory()).ReturnsAsync(mockCategories);

        var result = await _incomeCategoryService.GetAllCategory();
        
        Assert.That(result, Is.EqualTo(mockCategories));
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Salary"));
    }
}
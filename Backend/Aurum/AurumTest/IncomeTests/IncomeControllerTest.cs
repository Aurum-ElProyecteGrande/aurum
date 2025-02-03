using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurum.Controllers.Income;
using Aurum.Data.Entities;
using Aurum.Models.IncomeDTOs;
using Aurum.Repositories.Income.RegularIncome;
using Aurum.Repositories.Income.RegularIncome;
using Aurum.Repositories.IncomeRepository.IncomeRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Aurum.Services.Income;

namespace AurumTest.IncomeTests
{ 
    [TestFixture]
    public class IncomeControllerTest
    {
        private Mock<IIncomeRepo> _incomeRepo;
        private Mock<IRegularIncomeRepo> _regularIncomeRepo;
        private Mock<IIncomeService> _incomeService;
        private IncomeController _controller;

        [SetUp]
        public void Setup()
        {
            _incomeRepo = new();
            _regularIncomeRepo = new();
            _incomeService = new();
            _controller = new(_incomeRepo.Object, _regularIncomeRepo.Object, _incomeService.Object);
        }

        [Test]
        public async Task CreateInvalidReturnsBadRequest()
        {
            _incomeRepo.Setup(x => x.Create(It.IsAny<Income>())).ReturnsAsync(0);

            var result = await _controller.Create(It.IsAny<Income>());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateValidReturnsIncomeId()
        {
            int expected = 1;

            _incomeRepo.Setup(x => x.Create(It.IsAny<Income>())).ReturnsAsync(expected);

            var result = await _controller.Create(It.IsAny<Income>());

            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;

            var incomeId = (int)okResult?.Value;

            Assert.AreEqual(expected, incomeId);
        }


        [Test]
        public async Task CreateInvalidRegularReturnsBadRequest()
        {
            _regularIncomeRepo.Setup(x => x.CreateRegular(It.IsAny<ModifyRegularIncomeDto>())).ReturnsAsync(0);

            var result = await _controller.CreateRegular(It.IsAny<ModifyRegularIncomeDto>());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateValidRegularReturnsIncomeId()
        {
            int expected = 1;

            _regularIncomeRepo.Setup(x => x.CreateRegular(It.IsAny<ModifyRegularIncomeDto>())).ReturnsAsync(expected);

            var result = await _controller.CreateRegular(It.IsAny<ModifyRegularIncomeDto>());

            Assert.IsInstanceOf<OkObjectResult>(result);

            var okResult = result as OkObjectResult;

            var incomeId = (int)okResult?.Value;

            Assert.AreEqual(expected, incomeId);
        }

    }
}

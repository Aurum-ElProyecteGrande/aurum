using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurum.Controllers.Income;
using Aurum.Models.IncomeDTOs;
using Aurum.Repositories.Income;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AurumTest.IncomeTests
{
    [TestFixture]
    public class IncomeControllerTest
    {
        private Mock<IIncomeRepo> _incomeRepo;
        private Mock<IRegularIncomeRepo> _regularIncomeRepo;
        private IncomeController _controller;

        [SetUp]
        public void Setup()
        {
            _incomeRepo = new();
            _regularIncomeRepo = new();
            _controller = new(_incomeRepo.Object, _regularIncomeRepo.Object);
        }

        [Test]
        public async Task CreateInvalidReturnsBadRequest()
        {
            _incomeRepo.Setup(x => x.Create(It.IsAny<ModifyIncomeDto>())).ReturnsAsync(0);

            var result = await _controller.Create(It.IsAny<ModifyIncomeDto>());

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task CreateValidReturnsIncomeId()
        {
            int expected = 1;

            _incomeRepo.Setup(x => x.Create(It.IsAny<ModifyIncomeDto>())).ReturnsAsync(expected);

            var result = await _controller.Create(It.IsAny<ModifyIncomeDto>());

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

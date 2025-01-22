using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurum.Services.AccountService;
using Aurum.Services.BallanceService;
using Aurum.Services.ExpenseService;
using Aurum.Services.Income;
using Moq;

namespace AurumTest.BalanceTests
{
    [TestFixture]
    public class BalanceServiceTest
    {
        private Mock<IIncomeService> _incomeService;
        private Mock<IExpenseService> _expenseService;
        private Mock<IAccountService> _accountService;

        private BalanceService _balanceService;

        [SetUp]
        public void Setup()
        {
            _incomeService = new();
            _expenseService = new();
            _accountService = new();

            _balanceService = new(_incomeService.Object, _expenseService.Object, _accountService.Object);
        }

        [Test]
        public async Task CalculatesBalanceCorrectly()
        {
            decimal account = 100;
            decimal income = 20;
            decimal expense = 10;

            _incomeService.Setup(x => x.GetTotalIncome(It.IsAny<int>())).ReturnsAsync(income);
            _expenseService.Setup(x => x.GetTotalExpense(It.IsAny<int>())).ReturnsAsync(expense);
            _accountService.Setup(x => x.GetInitialAmount(It.IsAny<int>())).ReturnsAsync(account);

            var result = _balanceService.GetBalance(It.IsAny<int>()).Result;

            Assert.That(result, Is.EqualTo(account - expense + income));

        }

    }
}

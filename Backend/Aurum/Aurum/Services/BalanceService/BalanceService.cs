using Aurum.Models.BalanceDTOs;
using Aurum.Services.AccountService;
using Aurum.Services.ExpenseService;
using Aurum.Services.IncomeServices;

namespace Aurum.Services.BalanceService
{
    public class BalanceService : IBalanceService
    {
        IIncomeService _incomeService;
        IExpenseService _expenseService;
        IAccountService _accountService;

        public BalanceService(IIncomeService incomeService, IExpenseService expenseService, IAccountService accountService)
        {
            _incomeService = incomeService;
            _expenseService = expenseService;
            _accountService = accountService;
        }

        public DateTime ValidateDate(DateTime? date)
        {
            var validDate = new DateTime();

            if (date.HasValue) validDate = date.Value;

            if (!date.HasValue) throw new NullReferenceException("Missing date");

            return validDate;
        }

        public async Task<decimal> GetBalance(int accountId)
        {
            decimal initialAmount = await _accountService.GetInitialAmount(accountId);
            decimal totalExpense = await _expenseService.GetTotalExpense(accountId);
            decimal totalIncome = await _incomeService.GetTotalIncome(accountId);

            return initialAmount - totalExpense + totalIncome;
        }
        public async Task<decimal> GetBalance(int accountId, DateTime date)
        {
            decimal initialAmount = await _accountService.GetInitialAmount(accountId);
            decimal totalExpense = await _expenseService.GetTotalExpense(accountId, date);
            decimal totalIncome = await _incomeService.GetTotalIncome(accountId, date);

            return initialAmount - totalExpense + totalIncome;
        }

        public async Task<List<BalanceDto>> GetBalanceForRange(int accountId, DateTime startDate, DateTime endDate)
        {
            List<BalanceDto> balanceList = new();

            for (int i = 0; i < (endDate - startDate).Days; i++)
            {
                var curDate = startDate.AddDays(i);
                var balance = await GetBalance(accountId, curDate);
                BalanceDto balanceDto = new(curDate, balance);
                balanceList.Add(balanceDto);
            }

            return balanceList;
        }

    }
}


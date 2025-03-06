using Aurum.Data.Entities;
using Aurum.Models.AccountDto;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos
using Aurum.Models.IncomeDTOs;
using Aurum.Repositories.IncomeRepository.IncomeRepository;
using Aurum.Services.AccountService;
using Aurum.Services.CurrencyServices;
using Aurum.Services.IncomeCategoryServices;
using Microsoft.Identity.Client;
using System.Linq;
using System.Runtime.CompilerServices;
using Aurum.Models.CurrencyDtos;

namespace Aurum.Services.IncomeServices
{
    public class IncomeService : IIncomeService
    {
        IIncomeRepo _incomeRepo;
        IIncomeCategoryService _incomeCategoryService;
        IAccountService _accountService;
        ICurrencyService _currencyService;

        public IncomeService(IIncomeRepo incomeRepo, IIncomeCategoryService incomeCategoryService, IAccountService accountService, ICurrencyService currencyService)
        {
            _incomeRepo = incomeRepo;
            _incomeCategoryService = incomeCategoryService;
            _accountService = accountService;
            _currencyService = currencyService;
        }

        public (DateTime, DateTime) ValidateDates(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue || !endDate.HasValue)
                throw new ArgumentNullException("Start date and end date must not be null");

            return (startDate.Value, endDate.Value);
        }

        public async Task<decimal> GetTotalIncome(int accountId)
        {
            var incomes = await _incomeRepo.GetAll(accountId);
            return incomes
                .Select(i => i.Amount)
                .Sum();
        }
        public async Task<decimal> GetTotalIncome(int accountId, DateTime endDate)
        {
            var incomes = await _incomeRepo.GetAll(accountId, endDate);

            if (incomes == null)
            {
                return 0m;
            }

            return incomes
                .Select(i => i.Amount)
                .Sum();
        }

        public async Task<List<IncomeDto>> GetAll(int accountId)
        {
            var incomes = await _incomeRepo.GetAll(accountId);

            List<IncomeDto> incomeDtos = incomes
                .Select(i => ConvertIncomeToDto(i))
                .ToList();

            return incomeDtos;
        }

        public async Task<List<IncomeDto>> GetAll(int accountId, DateTime endDate)
        {
            var incomes = await _incomeRepo.GetAll(accountId, endDate);

            List<IncomeDto> incomeDtos = incomes
                .Select(i => ConvertIncomeToDto(i))
                .ToList();

            return incomeDtos;
        }

        public async Task<List<IncomeDto>> GetAll(int accountId, DateTime startDate, DateTime endDate)
        {
            if (accountId <= 0)
            {
                throw new ArgumentException("Invalid account ID");
            }
            if (startDate > endDate)
            {
                throw new ArgumentException("Start date cannot be after end date");
            }

            var incomes = await _incomeRepo.GetAll(accountId, startDate, endDate);

            List<IncomeDto> incomeDtos = incomes
                .Select(i => ConvertIncomeToDto(i))
                .ToList();

            return incomeDtos;
        }



        public async Task<int> Create(ModifyIncomeDto income)
        {
            var incomeId = await _incomeRepo.Create(ConvertModifyDtoToIncome(income));

            if (incomeId == 0) throw new InvalidOperationException("Invalid income input");

            return incomeId;
        }

        public async Task<bool> Delete(int incomeId)
        {
            var isDeleted = await _incomeRepo.Delete(incomeId);

            if (!isDeleted) throw new InvalidOperationException($"Could not delete income with id {incomeId}");

            return isDeleted;
        }

        private IncomeDto ConvertIncomeToDto(Income income)
        {
            var categoryDto = new CategoryDto(income.IncomeCategory.Name, income.IncomeCategoryId);
            var currencyDto = new CurrencyDto(income.Account.Currency.Name, income.Account.Currency.CurrencyCode, income.Account.Currency.Symbol);

            return new(categoryDto, currencyDto, income.Label, income.Amount, income.Date);
        }
        private Income ConvertModifyDtoToIncome(ModifyIncomeDto income) =>
            new Income()
            {
                AccountId = income.AccountId,
                IncomeCategoryId = income.CategoryId,
                Label = income.Label,
                Amount = income.Amount,
                Date = income.Date
            };

    }
}

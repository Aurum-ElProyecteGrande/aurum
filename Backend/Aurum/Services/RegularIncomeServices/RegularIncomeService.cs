using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDto;
using Aurum.Models.IncomeDTOs;
using Aurum.Models.RegularityEnum;
using Aurum.Repositories.IncomeRepository.RegularIncomeRepository;
using Aurum.Services.IncomeCategoryServices;
using Microsoft.Identity.Client;

namespace Aurum.Services.RegularIncomeServices
{
    public class RegularIncomeService : IRegularIncomeService
    {
        IRegularIncomeRepo _regularIncomeRepo;
        IIncomeCategoryService _incomeCategoryService;

        public RegularIncomeService(IRegularIncomeRepo regularIncomeRepo, IIncomeCategoryService incomeCategoryService)
        {
            _regularIncomeRepo = regularIncomeRepo;
            _incomeCategoryService = incomeCategoryService;
        }
        
        public async Task<List<RegularIncomeDto>> GetAllRegular(int accountId)
        {
            var incomes = await _regularIncomeRepo.GetAllRegular(accountId);
            List<RegularIncomeDto> incomeDtos = new();
            foreach (var income in incomes)
            {
                incomeDtos.Add(ConvertRegularIncomeToDto(income));
            }
            return incomeDtos;
        }
        
        public async Task<int> CreateRegular(ModifyRegularIncomeDto income)
        {
            var regularIncomeId = await _regularIncomeRepo.CreateRegular(ConvertModifyDtoToIncome(income));

            if (regularIncomeId == 0) throw new InvalidOperationException("Invalid regular income input");

            return regularIncomeId;
        }
        
        public async Task<int> UpdateRegular(ModifyRegularIncomeDto regularIncome, int regularId)
        {
            var regularToUpdate = await _regularIncomeRepo.Get(regularId);

            regularToUpdate.IncomeCategoryId = regularIncome.CategoryId;
            regularToUpdate.Label = regularIncome.Label;
            regularToUpdate.Amount = regularIncome.Amount;
            regularToUpdate.StartDate = regularIncome.StartDate;
            regularToUpdate.Regularity = regularIncome.Regularity;

            var regularIncomeId = await _regularIncomeRepo.UpdateRegular(regularToUpdate);

            if (regularIncomeId == 0) throw new InvalidOperationException("Invalid regular income input");

            return regularIncomeId;
        }
        
        public async Task<bool> DeleteRegular(int regularId)
        {
            var isDeleted = await _regularIncomeRepo.DeleteRegular(regularId);

            if (!isDeleted) throw new InvalidOperationException($"Could not delete income with id {regularId}");

            return isDeleted;
        }

        private RegularIncomeDto ConvertRegularIncomeToDto(RegularIncome income)
        {
            var category = new CategoryDto(income.IncomeCategory.Name, income.IncomeCategory.IncomeCategoryId);
            var currency = new CurrencyDto(income.Account.Currency.Name, income.Account.Currency.CurrencyCode, income.Account.Currency.Symbol);
            return new(income.RegularIncomeId, currency, category, income.Label, income.Amount, income.StartDate, income.Regularity);
        }

        private RegularIncome ConvertModifyDtoToIncome(ModifyRegularIncomeDto income) =>
            new RegularIncome()
            {
                AccountId = income.AccountId,
                IncomeCategoryId = income.CategoryId,
                Label = income.Label,
                Amount = income.Amount,
                StartDate = income.StartDate,
                Regularity = income.Regularity
            };


    }
}

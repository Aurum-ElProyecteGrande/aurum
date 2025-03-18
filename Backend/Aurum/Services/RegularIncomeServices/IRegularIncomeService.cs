using Aurum.Data.Entities;
using Aurum.Models.IncomeDTOs;

namespace Aurum.Services.RegularIncomeServices
{
    public interface IRegularIncomeService
    {
        Task<List<RegularIncomeDto>> GetAllRegularWithId(int accountId);
        Task<List<RegularIncome>> GetAllRegular();
        Task<int> CreateRegular(ModifyRegularIncomeDto income);
        Task<int> UpdateRegular(ModifyRegularIncomeDto regularIncome, int regularId);
        Task<bool> DeleteRegular(int regularId);
    }
}

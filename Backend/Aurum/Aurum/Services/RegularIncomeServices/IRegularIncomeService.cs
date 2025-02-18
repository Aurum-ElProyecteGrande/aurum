using Aurum.Data.Entities;
using Aurum.Models.IncomeDTOs;

namespace Aurum.Services.RegularIncomeServices
{
    public interface IRegularIncomeService
    {
        Task<List<RegularIncomeDto>> GetAllRegular(int accountId);
        Task<int> CreateRegular(ModifyRegularIncomeDto income);
        Task<int> UpdateRegular(ModifyRegularIncomeDto regularIncome, int regularId);
        Task<bool> DeleteRegular(int regularId);
    }
}

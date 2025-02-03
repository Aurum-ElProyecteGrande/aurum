using Aurum.Data.Entities;

namespace Aurum.Services.RegularIncomeServices
{
    public interface IRegularIncomeService
    {
        Task<List<RegularIncome>> GetAllRegular(int accountId);
        Task<int> CreateRegular(RegularIncome income);
        Task<int> UpdateRegular(int regularId, RegularIncome regularIncome);
        Task<bool> DeleteRegular(int regularId);
    }
}

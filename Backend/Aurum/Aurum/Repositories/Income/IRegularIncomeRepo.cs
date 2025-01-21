using Aurum.Models.IncomeDTOs;

namespace Aurum.Repositories.Income
{
    public interface IRegularIncomeRepo
    {
        Task<List<RegularIncomeDto>> GetAllRegular(int accountId);
        Task<int> CreateRegular(ModifyRegularIncomeDto income);
        Task<int> UpdateRegular(int regularId, ModifyRegularIncomeDto regularIncome);
        Task<bool> DeleteRegular(int regularId);

    }
}

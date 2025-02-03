using Aurum.Models.IncomeDTOs;
using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.RegularIncomeRepository
{
    public interface IRegularIncomeRepo
    {
        Task<List<RegularIncome>> GetAllRegular(int accountId);
        Task<int> CreateRegular(RegularIncome income);
        Task<int> UpdateRegular(RegularIncome regularIncome);
        Task<bool> DeleteRegular(int regularId);

    }
}

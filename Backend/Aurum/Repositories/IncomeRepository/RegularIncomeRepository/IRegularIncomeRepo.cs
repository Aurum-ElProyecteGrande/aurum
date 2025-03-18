using Aurum.Models.IncomeDTOs;
using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.RegularIncomeRepository
{
    public interface IRegularIncomeRepo
    {
        Task<RegularIncome> Get(int regularId);
        Task<List<RegularIncome>> GetAllRegularWithId(int accountId);
        Task<List<RegularIncome>> GetAllRegular();
        Task<int> CreateRegular(RegularIncome income);
        Task<int> UpdateRegular(RegularIncome regularIncome);
        Task<bool> DeleteRegular(int regularId);

    }
}

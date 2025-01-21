using Aurum.Models.IncomeDTOs;

namespace Aurum.Repositories.Income
{
    public interface IRegularIncomeRepo
    {
        List<RegularIncomeDto> GetAllRegular(int accountId);
        int CreateRegular(ModifyRegularIncomeDto income);
        int UpdateRegular(int regularId, ModifyRegularIncomeDto regularIncome);
        bool DeleteRegular(int regularId);

    }
}

using Aurum.Models.IncomeDTOs;
using System;

namespace Aurum.Repositories.Income
{
    public interface IIncomeRepo
    {
        List<IncomeDto> GetAll(int accountId);
        List<IncomeDto> GetAll(int accountId, DateTime? startDate, DateTime? endDate);
        int Create(ModifyIncomeDto income);
        bool Delete(int incomeId);
    }
}

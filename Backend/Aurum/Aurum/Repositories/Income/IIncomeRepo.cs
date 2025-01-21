using Aurum.Models.IncomeDTOs;
using System;

namespace Aurum.Repositories.Income
{
    public interface IIncomeRepo
    {
        Task<List<IncomeDto>> GetAll(int accountId);
        Task<List<IncomeDto>> GetAll(int accountId, DateTime? startDate, DateTime? endDate);
        Task<int> Create(ModifyIncomeDto income);
        Task<bool> Delete(int incomeId);
    }
}

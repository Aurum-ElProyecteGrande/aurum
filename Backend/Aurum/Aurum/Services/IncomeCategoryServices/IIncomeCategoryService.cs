using Aurum.Data.Entities;

namespace Aurum.Services.IncomeCategoryServices
{
    public interface IIncomeCategoryService
    {
        Task<List<IncomeCategory>> GetAllCategory();
    }
}

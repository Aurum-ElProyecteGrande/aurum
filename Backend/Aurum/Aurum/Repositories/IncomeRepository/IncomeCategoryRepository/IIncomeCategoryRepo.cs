using Aurum.Data.Entities;

namespace Aurum.Repositories.IncomeRepository.IncomeCategoryRepository
{
    public interface IIncomeCategoryRepo
    {
        Task<List<IncomeCategory>> GetAllCategory();
    }
}

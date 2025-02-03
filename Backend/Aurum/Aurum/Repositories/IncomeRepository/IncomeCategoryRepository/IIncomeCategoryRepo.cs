namespace Aurum.Repositories.IncomeRepository.IncomeCategoryRepository
{
    public interface IIncomeCategoryRepo
    {
        Task<List<Data.Entities.IncomeCategory>> GetAllCategory();

    }
}

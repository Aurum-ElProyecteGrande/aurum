using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record IncomeWithCurrency(Currency Currency, CategoryDto Category, string Label, decimal Amount, DateTime Date);
}

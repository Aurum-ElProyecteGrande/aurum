using Aurum.Models.CategoryDTOs;

namespace Aurum.Models.IncomeDTOs
{
    public record RegularIncomeDto(int RegularId, int AcccountId, CategoryDto Category, string Label, decimal Amount, DateTime StartDate, Regularity Regularity);

}
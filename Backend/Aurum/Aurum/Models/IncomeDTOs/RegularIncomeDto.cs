using Aurum.Models.RegularityEnum;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record RegularIncomeDto(int RegularId, int AcccountId, CategoryDto Category, string Label, decimal Amount, DateTime StartDate, Regularity Regularity);

}
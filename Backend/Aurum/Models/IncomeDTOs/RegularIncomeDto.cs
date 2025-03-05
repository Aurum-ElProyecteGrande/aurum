using Aurum.Models.RegularityEnum;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record RegularIncomeDto(int RegularId, CurrencyDto Currency, CategoryDto Category, string Label, decimal Amount, DateTime StartDate, Regularity Regularity);

}
using System;
using Aurum.Models.AccountDto;
using Aurum.Models.CurrencyDtos;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record IncomeDto (CategoryDto Category, CurrencyDto Currency, string Label, decimal Amount, DateTime Date);

}

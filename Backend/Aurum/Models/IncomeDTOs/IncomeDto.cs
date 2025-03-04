using System;
using Aurum.Models.AccountDto;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record IncomeDto (CategoryDto Category, CurrencyDto Currency, string Label, decimal Amount, DateTime Date);

}

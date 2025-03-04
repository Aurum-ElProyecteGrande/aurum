using System;
using Aurum.Models.AccountDto;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record IncomeDto (CategoryDto Category, CurrencyDto.CurrencyDto Currency, string Label, decimal Amount, DateTime Date);

}

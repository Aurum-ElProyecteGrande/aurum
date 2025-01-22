using System;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.IncomeDTOs
{
    public record IncomeDto (CategoryDto Category, string Label, decimal Amount, DateTime Date);

}

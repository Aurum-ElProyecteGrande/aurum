using System;
using Aurum.Models.CategoryDTOs;

namespace Aurum.Models.IncomeDTOs
{
    public record IncomeDto (CategoryDto Category, string Label, decimal Amount, DateTime Date);

}

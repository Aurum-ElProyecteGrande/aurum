using System;
using Aurum.Models.CategoryDTOs;

namespace Aurum.Models.IncomeDTOs
{
    public record ModifyIncomeDto(int AccountId, int CategoryId, string Label, decimal Amount, DateTime Date);
}

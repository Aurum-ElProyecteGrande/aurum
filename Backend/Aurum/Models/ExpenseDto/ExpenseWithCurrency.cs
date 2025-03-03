using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.ExpenseDto
{
    public record ExpenseWithCurrency(
        Currency Currency,
        CategoryDto Category,
        SubCategoryDto? Subcategory,
        string Label,
        decimal Amount,
        DateTime Date
    );
}

using Aurum.Data.Entities;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;

namespace Aurum.Models.ExpenseDTO
{
	public record ExpenseDto(
		CurrencyDto Currency,
		CategoryDto Category,
		SubCategoryDto? Subcategory,
		string Label,
		decimal Amount,
		DateTime Date
	);
}

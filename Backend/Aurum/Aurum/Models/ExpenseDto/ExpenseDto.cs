

using Aurum.Models.CategoryDtos;

namespace Aurum.Models.ExpenseDtos
{
	public record ExpenseDto(
		CategoryDto Category,
		SubCategoryDto? Subcategory,
		string Label,
		decimal Amount,
		DateTime Date
	);
}

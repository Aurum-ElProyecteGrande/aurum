

using Aurum.Models.CategoryDto;

namespace Aurum.Models.ExpenseDtos
{
	public record ExpenseDto(
		CategoryDto.CategoryDto Category,
		SubCategoryDto? Subcategory,
		string Label,
		decimal Amount,
		DateTime Date
	);
}

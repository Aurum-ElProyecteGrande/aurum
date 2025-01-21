namespace Aurum.Models.ExpenseDto;

public record RawExpenseDto(
	int CategoryId,
	int SubCategoryId,
	string Label,
	decimal Amount,
	DateTime Date
		);
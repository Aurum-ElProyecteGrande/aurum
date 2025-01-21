namespace Aurum.Models.ExpenseDto;

public record ModifyExpenseDto(
	int AccountId,
	int CategoryId,
	string? SubCategoryName,
	string Label,
	decimal Amount,
	DateTime Date
	);
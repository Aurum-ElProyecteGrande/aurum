using Aurum.Models.Regular_enum;

namespace Aurum.Models.RegularExpenseDto;

public record ModifyRegularExpenseDto(
	int AccountId,
	int CategoryId,
	string? SubCategoryName,
	string Label,
	decimal Amount,
	DateTime StartDate,
	Regularity Regularity
	);
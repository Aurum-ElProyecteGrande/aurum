using Aurum.Models.RegularityEnum;

namespace Aurum.Models.RegularExpenseDto;

public record ModifyRegularExpenseDto(
	int RegularId,
	int AccountId,
	int CategoryId,
	string? SubCategoryName,
	string Label,
	decimal Amount,
	DateTime StartDate,
	Regularity Regularity
	);
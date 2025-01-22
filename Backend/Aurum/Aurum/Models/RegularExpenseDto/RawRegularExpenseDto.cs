using Aurum.Models.Regular_enum;

namespace Aurum.Models.RegularExpenseDto;

public record RawRegularExpenseDto(
	int RegularId,
	int AccountId,
	int CategoryId,
	int? SubcategoryId,
	string Label,
	decimal Amount,
	DateTime StartDate,
	Regularity Regularity
	);
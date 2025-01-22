using Aurum.Models.RegularityEnum;

namespace Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;

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
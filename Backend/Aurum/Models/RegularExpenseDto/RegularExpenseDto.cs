using System.Text.Json.Serialization;
using Aurum.Models.CategoryDtos;
using Aurum.Models.CurrencyDtos;
using Aurum.Models.CustomJsonConverter;
using Aurum.Models.RegularityEnum;

namespace Aurum.Models.RegularExpenseDto;

public record RegularExpenseDto(
	int RegularId,
	CurrencyDto Currency,
	CategoryDto Category,
	SubCategoryDto? Subcategory,
	string Label,
	decimal Amount,
	DateTime StartDate,
	Regularity Regularity
	);

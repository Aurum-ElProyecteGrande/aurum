using System.Text.Json.Serialization;
using Aurum.Models.CategoryDto;
using Aurum.Models.CustomJsonConverter;
using Aurum.Models.Regular_enum;

namespace Aurum.Models.RegularExpenseDto;

public record RegularExpenseDto(
	int RegularId,
	int AccountId,
	CategoryDto.CategoryDto Category,
	SubCategoryDto? Subcategory,
	string Label,
	decimal Amount,
	DateTime StartDate,
	Regularity Regularity
	);
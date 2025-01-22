namespace Aurum.Models.CategoryDtos;

public record SubCategoryDto(
	string Name,
	int SubCategoryId,
	int CategoryId
	);
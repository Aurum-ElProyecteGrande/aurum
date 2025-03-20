namespace Aurum.Models.CurrencyDtos;

public record CurrencyDto(
	int CurrencyId,
    string Name,
    string CurrencyCode,
    string Symbol
    );

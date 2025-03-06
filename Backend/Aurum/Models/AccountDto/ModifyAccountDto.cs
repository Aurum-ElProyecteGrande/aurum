namespace Aurum.Models.AccountDto;

public record ModifyAccountDto(
    string DisplayName,
    decimal Amount,
    int CurrencyId
    );
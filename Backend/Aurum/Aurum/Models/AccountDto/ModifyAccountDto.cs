namespace Aurum.Models.AccountDto;

public record ModifyAccountDto(
    string UserId,
    string DisplayName,
    decimal Amount,
    int CurrencyId
    );
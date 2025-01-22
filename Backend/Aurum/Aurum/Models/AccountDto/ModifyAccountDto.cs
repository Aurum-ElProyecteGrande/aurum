namespace Aurum.Models.AccountDto;

public record ModifyAccountDto(
    int UserId,
    string DisplayName,
    decimal Amount,
    int CurrencyId
    );
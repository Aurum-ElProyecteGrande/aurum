namespace Aurum.Models.AccountDto;

public record AccountDto(
    int AccountId,
    int UserId,
    string DisplayName,
    decimal Amount,
    int CurrencyId
    );

using Aurum.Data.Entities;
using Aurum.Models.CurrencyDto;

namespace Aurum.Models.AccountDto;

public record AccountDto(
    int AccountId,
    string UserId,
    string DisplayName,
    decimal Amount,
    CurrencyDto.CurrencyDto Currency
    );

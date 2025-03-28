using Aurum.Data.Entities;
using Aurum.Models.CurrencyDtos;

namespace Aurum.Models.AccountDto;

public record AccountDto(
    int AccountId,
    string UserId,
    string DisplayName,
    decimal Amount,
    CurrencyDto Currency
    );

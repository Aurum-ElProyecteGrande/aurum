using Aurum.Data.Entities;

namespace Aurum.Models.AccountDto;

public record AccountDto(
    int AccountId,
    string UserId,
    string DisplayName,
    decimal Amount,
    Currency Currency
    );

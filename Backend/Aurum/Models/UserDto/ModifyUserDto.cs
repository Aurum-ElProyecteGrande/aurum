namespace Aurum.Models.UserDto;

public record ModifyUserDto(
    string DisplayName,
    string Email,
    string? Password
    );
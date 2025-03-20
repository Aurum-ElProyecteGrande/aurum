using Aurum.Data.Contracts;
using Aurum.Models.AccountDto;

namespace Aurum.Models.UserDTO
{
	public record UserCreationDto(RegistrationRequest RegistrationRequest, ModifyAccountDto ModifyAccountDto);
}

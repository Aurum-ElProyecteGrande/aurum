using System.ComponentModel.DataAnnotations;

namespace Aurum.Data.Contracts;

public record RegistrationRequest(
	[Required]string Email,
	[Required]string Username,
	[Required]string Password,
	[Required]string Role
	);
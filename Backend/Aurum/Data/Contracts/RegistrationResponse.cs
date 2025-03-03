using System.ComponentModel.DataAnnotations;

namespace Aurum.Data.Contracts;

public record RegistrationResponse(
	string Email,
	string Username
);
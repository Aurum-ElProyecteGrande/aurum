namespace Aurum.Data.Contracts;

public record AuthResult(
	bool Success,
	string Email,
	string UserName,
	string Token,
	string UserId)
{
	public readonly Dictionary<string, string> ErrorMessages = new();
}
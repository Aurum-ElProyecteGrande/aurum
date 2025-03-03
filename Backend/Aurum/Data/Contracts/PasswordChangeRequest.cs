using System.ComponentModel.DataAnnotations;

namespace Aurum.Data.Contracts;

public class PasswordChangeRequest
{
	[Required(ErrorMessage = "Old password is required.")]
	public string OldPassword { get; set; }

	[Required(ErrorMessage = "New password is required.")]
	[StringLength(100, ErrorMessage = "New password must be at least 6 characters long.", MinimumLength = 6)]
	public string NewPassword { get; set; }
}
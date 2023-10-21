using HFS_BE.Base;
using HFS_BE.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Dao.AuthDao
{
    public class AuthDaoInputDto : BaseInputDto
    {

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
		public string Password { get; set; }
    }

    public class RegisterDto
	{

		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Gender is required")]
		public string? Gender { get; set; }

		[Required(ErrorMessage = "Birth date is required")]
		[MinimumAge(18, ErrorMessage = "You must be at least 18 years old")]
		public DateTime? BirthDate { get; set; }

		[Required(ErrorMessage = "Role ID is required")]
		public int RoleId { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = null!;

		[Required(ErrorMessage = "Password is required")]
		[StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
		public string Password { get; set; } = null!;

	}
	public class ForgotPasswordInputDto:BaseInputDto
	{ 
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

	}

}

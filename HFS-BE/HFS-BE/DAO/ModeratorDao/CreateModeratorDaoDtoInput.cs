using HFS_BE.Base;
using HFS_BE.Utils.Validation;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.DAO.ModeratorDao
{
	public class CreateModeratorDaoDtoInput:BaseInputDto
	{
		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Gender is required")]
		public string? Gender { get; set; }

		[Required(ErrorMessage = "Birth date is required")]
		[MinimumAge(18, 100, ErrorMessage = "You must be at least 18 years old and not older than 100 years old ")]
		public DateTime? BirthDate { get; set; }

		[PhoneNumber(ErrorMessage = "Invalid phone number.")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = null!;

		[Password(8, ErrorMessage = "Invalid password.")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Confirm password does not match.")]
		public string ConfirmPassword { get; set; }
	}
	public class BanModeratorDtoinput : BaseInputDto
	{
		public string ModId { get; set; }
		public bool? IsBanned { get; set; }
	}
	public class BanAccountantDtoinput : BaseInputDto
	{
		public string AccountantId { get; set; }
		public bool? IsBanned { get; set; }
	}
	public class UpdateModeratorDtoinput : BaseInputDto
	{
		public string ModId { get; set; }

		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Gender is required")]
		public string? Gender { get; set; }

		[Required(ErrorMessage = "Birth date is required")]
		[MinimumAge(18, 100, ErrorMessage = "You must be at least 18 years old and not older than 100 years old ")]
		public DateTime? BirthDate { get; set; }

		[PhoneNumber(ErrorMessage = "Invalid phone number.")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = null!;

		[Password(8, ErrorMessage = "Invalid password.")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Confirm password does not match.")]
		public string ConfirmPassword { get; set; }

	}
	public class UpdateModeratorDtoinputNotPassword : BaseInputDto
	{
		public string ModId { get; set; }

		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Gender is required")]
		public string? Gender { get; set; }

		[Required(ErrorMessage = "Birth date is required")]
		[MinimumAge(18, 100, ErrorMessage = "You must be at least 18 years old and not older than 100 years old ")]
		public DateTime? BirthDate { get; set; }

		[PhoneNumber(ErrorMessage = "Invalid phone number.")]
		public string? PhoneNumber { get; set; }

		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = null!;

		

	}
}

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
		[MinimumAge(18, 100, ErrorMessage = "You must be at least 18 years old and not older than 100 years old ")]
		public DateTime? BirthDate { get; set; }
		[Required(ErrorMessage = "Phone is required")]
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

	public class RegisterSellerDto
	{

		[Required(ErrorMessage = "First name is required")]
		public string FirstName { get; set; } = null!;

		[Required(ErrorMessage = "Last name is required")]
		public string LastName { get; set; } = null!;

		[Required(ErrorMessage = "Gender is required")]
		public string? Gender { get; set; }

		[Required(ErrorMessage = "Birth date is required")]
		[MinimumAge(18,100, ErrorMessage = "You must be at least 18 years old and not older than 100 years old ")]
		public DateTime? BirthDate { get; set; }


		[Required(ErrorMessage = "Phone is required")]
		[PhoneNumber(ErrorMessage = "Invalid phone number.")]
      	public string? PhoneNumber { get; set; }


		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = null!;

		[Password(8, ErrorMessage = "Invalid password.")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage = "Confirm password does not match.")]
		public string ConfirmPassword { get; set; }
		[Required(ErrorMessage = "ShopName is required")]
		public string? ShopName { get; set; }
		[Required(ErrorMessage = "ShopAddress is required")]
		public string? ShopAddress { get; set; }
	}
	public class ForgotPasswordInputDto:BaseInputDto
	{ 
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

    }

    public class ConfirmForgotPasswordInputDto : BaseInputDto
    {
        public string confirm { get; set; }
        public string UserId { get; set; }
    }
    public class LoginGoogleInputDto : BaseInputDto
    {
        public string UserId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public bool? IsBanned { get; set; }
    }

    public class ChangeForgotPasswordInputDto : BaseInputDto
    {
        [Required(ErrorMessage = "Confirm is required")]
        public string confirm { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 20 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; }
    }
}

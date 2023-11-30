using HFS_BE.Base;
using HFS_BE.Utils;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.BusinessLogic.Auth
{
	public class LoginInPutDto:BaseInputDto
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
	public class RegisterInputDto:BaseInputDto
	{

		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public string? PhoneNumber { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string ConfirmPassword { get; set; }

	}
	public class RegisterSellerInputDto : BaseInputDto
	{

	
		public string? PhoneNumber { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;
		public string ConfirmPassword { get; set; }
		public string? ShopName { get; set; }
		public string? ShopAddress { get; set; }

		public IReadOnlyList<IFormFile>? Images { get; set; } = null;
	}

    public class TokenApiModelBL
    {
        public string? RefreshToken { get; set; }
    }

	public class RevokeToken
	{
		public string? Id { get; set; }
		public string? Role { get; set; }
	}
}

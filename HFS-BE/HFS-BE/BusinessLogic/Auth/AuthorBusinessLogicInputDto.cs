using HFS_BE.Base;
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
		public DateTime? BirthDate { get; set; }
		public int RoleId { get; set; }
		public string Email { get; set; } = null!;
		public string Password { get; set; } = null!;

	}
}

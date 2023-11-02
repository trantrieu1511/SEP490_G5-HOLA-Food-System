using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.ManageUser
{
	public class CreateModerator: BaseInputDto
	{
		

			public string FirstName { get; set; } = null!;
			public string LastName { get; set; } = null!;
			public string? Gender { get; set; }
			public DateTime? BirthDate { get; set; }
		    public string? PhoneNumber { get; set; }
		    public string Email { get; set; } = null!;
			public string Password { get; set; } = null!;
		    public string CofirmPassword { get; set; } = null!;

	}
}

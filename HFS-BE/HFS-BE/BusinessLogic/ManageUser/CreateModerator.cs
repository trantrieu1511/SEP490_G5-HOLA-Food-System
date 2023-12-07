using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.ManageUser
{
	public class CreateModerator: BaseInputDto
	{
		

			public string FirstName { get; set; } 
			public string LastName { get; set; } 
			public string? Gender { get; set; }
			public DateTime? BirthDate { get; set; }
		    public string? PhoneNumber { get; set; }
		    public string Email { get; set; } 
	     	public string Password { get; set; }
		    public string ConfirmPassword { get; set; }

	}
}

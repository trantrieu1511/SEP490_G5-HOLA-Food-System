using HFS_BE.Base;

namespace HFS_BE.DAO.ModeratorDao
{
	public class MenuModeratorDtoOutput: BaseOutputDto
	{
		public string ModId { get; set; } = null!;
		public string FirstName { get; set; } = null!;
		public string LastName { get; set; } = null!;
		public string? Gender { get; set; }
		public DateTime? BirthDate { get; set; }
		public string Email { get; set; } = null!;
		public long? PhoneNumber { get; set; }
		public string? Avatar { get; set; }
		public bool IsOnline { get; set; }
		public bool ConfirmEmail { get; set; }

	}
}

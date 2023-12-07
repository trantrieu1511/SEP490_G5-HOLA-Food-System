using HFS_BE.Base;

namespace HFS_BE.DAO.AdminDao
{
	public class DashboadAdminInputDto
	{
	}

	public class DashboadAdminInputOrderDto
	{
		public DateTime DateOrder { get; set; }
	}


	public class DashBoardAdminLineInputDto : BaseInputDto
	{
		public List<DateTime>? dates { get; set; }
	}
}

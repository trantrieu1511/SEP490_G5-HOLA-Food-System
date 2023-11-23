using HFS_BE.Base;

namespace HFS_BE.DAO.AdminDao
{
	public class DashboadPieAdminOutputDto
	{

		public DashboadPieAdminOutputDto(string? actor, int total)
		{
			Actor = actor;
			Total = total;
		}

		public string? Actor { get; set; }
		public int Total { get; set; }

	}
	public class DashboadAdminOutputDto
	{
		public int TotalSeller { get; set; }
		public int BanSeller { get; set; }
		public int VetifySeller { get; set; }
		public int BanCustomer { get; set; }
		public int TotalCustomer { get; set; }

		public int TotalShippper { get; set; }
		public int BanShipper { get; set; }
		public int VetifyShipper { get; set; }
	}
	public class ListDashboadPieAdminOutputDto :BaseOutputDto
	{
		public List<DashboadPieAdminOutputDto> Pies { get; set; }

	}
	public class DoashboadOrderbyAdmin : BaseOutputDto
	{
		public int TotalFood { get; set; }
		public int TotalOrder { get; set; }

	}
}

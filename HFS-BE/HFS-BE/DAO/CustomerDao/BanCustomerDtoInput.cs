using HFS_BE.Base;

namespace HFS_BE.DAO.CustomerDao
{
	public class BanCustomerDtoInput:BaseInputDto
	{
		public string CustomerId { get; set; } = null!;
		public bool? Ban { get; set; }
	}
}

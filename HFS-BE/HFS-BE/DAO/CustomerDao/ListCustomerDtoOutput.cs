using HFS_BE.Base;

namespace HFS_BE.DAO.CustomerDao
{
	public class ListCustomerDtoOutput:BaseOutputDto	{
		public List<CustomerDtoOutput> Customers { get; set; }
	}
}

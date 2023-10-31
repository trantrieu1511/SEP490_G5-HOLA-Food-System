using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManageUser.ManageCustomer
{
	public class CustomerBusinessLogic : BaseBusinessLogic
	{
		public CustomerBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListCustomerDtoOutput ListCustomer()
		{
			try
			{
				var Dao = this.CreateDao<CustomerDao>();
				var daooutput = Dao.GetAllCustomer();

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

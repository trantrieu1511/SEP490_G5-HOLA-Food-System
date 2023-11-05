using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;

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
		public BaseOutputDto BanCustomer(BanCustomerDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<CustomerDao>();
				var daooutput = Dao.BanCustomer(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ListHistoryBanCustomer ListHistoryBanCustomer(BanCustomerHistoryDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<CustomerDao>();
				var daooutput = Dao.ListHistoryCustomer(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

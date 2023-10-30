using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;

namespace HFS_BE.DAO.CustomerDao
{
	public class CustomerDao : BaseDao
	{
		public CustomerDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListCustomerDtoOutput GetAllCustomer()
		{
			try
			{
				var user = this.context.Customers.ToList();

				var output = this.Output<ListCustomerDtoOutput>(Constants.ResultCdSuccess);
				output.data = mapper.Map<List<Customer>, List<CustomerDtoOutput>>(user);
				return output;
			}
			catch (Exception)
			{
				return this.Output<ListCustomerDtoOutput>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto BanCustomer(BanCustomerDtoInput input)
		{
			try
			{
				var user = this.context.Customers.FirstOrDefault(s=>s.CustomerId==input.CustomerId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Customer is not in data ");
				}
				user.IsBanned = input.Ban;
				context.Customers.Update(user);
				context.SaveChanges();
				var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
				
				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

	}
}

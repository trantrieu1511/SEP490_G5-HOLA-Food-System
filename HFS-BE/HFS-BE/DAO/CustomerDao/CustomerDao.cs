using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ProfileImage;
using HFS_BE.DAO.SellerDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using HFS_BE.Utils.IOFile;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;
using Twilio.Rest.Api.V2010.Account;

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
				var user = context.Customers.Include(s => s.Orders).ThenInclude(s => s.OrderProgresses)
						 .Select(p => new CustomerDtoOutput
						 {
							 CustomerId = p.CustomerId,
							 FirstName = p.FirstName,
							 LastName = p.LastName,
							 Gender = p.Gender,
							 PhoneNumber = p.PhoneNumber,
							 BirthDate = p.BirthDate,
							 Email = p.Email,
							 ConfirmedEmail = (p.ConfirmedEmail),
							 IsPhoneVerified=p.IsPhoneVerified,
							 NumberOfViolations = p.NumberOfViolations,
							 Orders = p.Orders
						.Where(s => s.OrderProgresses.Where(s => s.Status == 5).Any())
								 .Select(s => new OrderCustomerOutputDto
								 {
									 OrderId = s.OrderId,
									 OrderDate = s.OrderDate,
									 Note = s.OrderProgresses.FirstOrDefault(s => s.Status == 5).Note,
									 ShipAddress = s.ShipAddress,
									 ShipperId = s.ShipperId
								 }).ToList(),
							 Images = context.ProfileImages
						.Where(pi => pi.UserId == p.CustomerId && pi.IsReplaced == false)
					   .Select(pi => new ImageCustomerOutputDto
					   {
						   ImageId = pi.ImageId,
						   UserId = pi.UserId,
						   Path = pi.Path,
						   IsReplaced = pi.IsReplaced
					   })
					 .ToList()

						 })

						.ToList();
				var output = this.Output<ListCustomerDtoOutput>(Constants.ResultCdSuccess);
				output.Customers = user;

				return output;
			}
			catch (Exception)
			{
				return this.Output<ListCustomerDtoOutput>(Constants.ResultCdFail);
			}
		}
	

		//public BaseOutputDto BanCustomer(BanCustomerDtoInput input)
		//{
		//	try
		//	{
		//		CustomerBan ban = new CustomerBan();
		//		var user = context.Customers.FirstOrDefault(s=>s.CustomerId==input.CustomerId);
		//		if (user == null)
		//		{
		//			return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Customer is not in data ");
		//		}


		//			ban.CustomerId = user.CustomerId;
		//			ban.Reason = input.Reason;
		//		    ban.CreateDate = DateTime.Now;
		//			//user.IsBanned = input.IsBanned;
		//			context.Customers.Update(user);
		//			context.CustomerBans.Add(ban);
		//			context.SaveChanges();



		//		var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

		//		return output;
		//	}
		//	catch (Exception)
		//	{
		//		return this.Output<BaseOutputDto>(Constants.ResultCdFail);
		//	}
		//}
		//public ListHistoryBanCustomer ListHistoryCustomer(BanCustomerHistoryDtoInput cusId)
		//{
		//	try
		//	{

		//		var user = context.CustomerBans.Where(s=>s.CustomerId==cusId.CustomerId).ToList();

		//		var output = this.Output<ListHistoryBanCustomer>(Constants.ResultCdSuccess);
		//		output.data = mapper.Map<List<CustomerBan>, List<BanHistoryCustomerDtoOutput>>(user);



		//		return output;
		//	}
		//	catch (Exception)
		//	{
		//		return this.Output<ListHistoryBanCustomer>(Constants.ResultCdFail);
		//	}
		//}
		public async Task<CustomerMessageDtoOutput> GetCustoemrAsync(string email)
		{
			var user = await context.Customers
				.FirstOrDefaultAsync(x => x.Email == email);

			var datmapper = mapper.Map<Customer, CustomerMessageDtoOutput>(user);
			return datmapper;
		}


		public async Task<List<CustomerMessageDtoOutput>> ListCustomersendSellerbySellerAsync(string emailseller)
		{
			var user = await context.Customers.Include(s=>s.ChatMessages).ThenInclude(s=>s.Seller).
				Where(s=>s.ChatMessages.Any(cm => cm.Seller.Email == emailseller))
	.ToListAsync();
		

			var datmapper = mapper.Map<List<Customer>,List<CustomerMessageDtoOutput>>(user);
			foreach (var u in datmapper)
			{
				var img = await context.ProfileImages.Where(s => s.UserId == u.CustomerId && s.IsReplaced == false).FirstOrDefaultAsync();
				ImageFileConvert.ImageOutputDto? imageInfor = null;
				if (img == null)
				{
					break;

				}
				imageInfor = ImageFileConvert.ConvertFileToBase64(img.UserId, img.Path, 3);
				//	var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>(imageInfor);
				u.Image = imageInfor.ImageBase64;
			}
			return datmapper;
		}
	}
}

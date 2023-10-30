using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.SellerDao
{
	public class SellerDao : BaseDao
	{
		public SellerDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListSellerDtoOutput GetAllSeller()
		{
			try
			{
				var user = this.context.Sellers.ToList();

				var output = this.Output<ListSellerDtoOutput>(Constants.ResultCdSuccess);
				output.data = mapper.Map<List<Seller>, List<SellerDtoOutput>>(user);
				return output;
			}
			catch (Exception)
			{
				return this.Output<ListSellerDtoOutput>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto BanSeller(BanSellerDtoInput input)
		{
			try
			{
				var user = this.context.Sellers.FirstOrDefault(s => s.SellerId == input.SellerId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Seller is not in data ");
				}
				user.IsBanned = input.Ban;
				context.Sellers.Update(user);
				context.SaveChanges();
				var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		public BaseOutputDto ActiveSeller(ActiveSellerDtoInput input)
		{
			try
			{
				var user = this.context.Sellers.FirstOrDefault(s => s.SellerId == input.SellerId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Seller is not in data ");
				}
				user.IsVerified = input.IsVerified;
				context.Sellers.Update(user);
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

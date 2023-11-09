using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
			var validationContext = new ValidationContext(input, serviceProvider: null, items: null);
			var validationResults = new List<ValidationResult>();
			bool isValid = Validator.TryValidateObject(input, validationContext, validationResults, validateAllProperties: true);
			if (!isValid)
			{
				string err = "";
				foreach (var item in validationResults)
				{
					err += item.ToString() + " ";
				}
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, err);
			}
			try
			{
				SellerBan ban = new SellerBan();
				var user = this.context.Sellers.FirstOrDefault(s => s.SellerId == input.SellerId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Seller is not in data ");
				}
				ban.SellerId = user.SellerId;
				ban.Reason = input.Reason;
				user.IsBanned = input.IsBanned;
				ban.CreateDate = DateTime.Now;
				context.Sellers.Update(user);
				context.SellerBans.Add(ban);
				context.SaveChanges();
				var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				return output;
			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail,ex.ToString());
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

		public ListHistoryBanSeller ListHistorySeller(BanSellerHistoryDtoInput Id)
		{
			try
			{

				var user = this.context.SellerBans.Where(s => s.SellerId == Id.SellerId).ToList();

				var output = this.Output<ListHistoryBanSeller>(Constants.ResultCdSuccess);
				output.data = mapper.Map<List<SellerBan>, List<BanHistorySellerDtoOutput>>(user);



				return output;
			}
			catch (Exception)
			{
				return this.Output<ListHistoryBanSeller>(Constants.ResultCdFail);
			}
		}






		public async Task<SellerDtoOutput> GetSellersAsync(string email)
		{
			var user = await context.Sellers
				.FirstOrDefaultAsync(x => x.Email == email);

			var datmapper = mapper.Map<Seller, SellerDtoOutput>(user);
			return datmapper;
		}
		//public async Task<List<SellerDtoOutput>> GetUsersOnlineAsync(string currentEmail, string[] userOnline)
		//{
		//	try
		//	{
		//		var listUserOnline = new List<SellerDtoOutput>();
		//		//foreach (var u in userOnline)
		//		//{
		//		//	var user = await context.Sellers.Where(x => x.Email == u).SingleOrDefaultAsync();
		//		//	var datmapper = mapper.Map<Seller, SellerDtoOutput>(user);
		//		//	if (datmapper != null)
		//		//	{
		//		//		listUserOnline.Add(datmapper);
		//		//	}

		//		//}
		//		var listseller = await context.Sellers.Where(x => x.IsBanned == false).ToListAsync();
		//		HashSet<string> emailsAdded = new HashSet<string>();

		//		foreach (var s in listseller)
		//		{
		//			var datmapper = mapper.Map<Seller, SellerDtoOutput>(s);

		//			// Nếu email đã được thêm vào listUserOnline, loại bỏ email đó
		//			if (emailsAdded.Contains(datmapper.Email))
		//			{
		//				listUserOnline.RemoveAll(item => item.Email == datmapper.Email);
		//			}

		//			// Thêm email vào HashSet và danh sách listUserOnline
		//			emailsAdded.Add(datmapper.Email);
		//			datmapper.IsOnline = userOnline.Any(u => u == datmapper.Email);
		//			listUserOnline.Add(datmapper);
		//		}

		//		return await Task.Run(() => listUserOnline.Where(x => x.Email != currentEmail).ToList());
		//	}
		//	catch (Exception)
		//	{
		//		return null;
		//	}
		//}
		public async Task<List<SellerDtoOutput>> GetUsersOnlineAsync(string currentEmail, string[] userOnline)
		{
			try
			{
				var listUserOnline = new List<SellerDtoOutput>();


				foreach (var u in userOnline)
				{
					var seller = await context.Sellers.Where(s=>s.Email==u).SingleOrDefaultAsync();
					var datmapper = mapper.Map<Seller, SellerDtoOutput>(seller);
					datmapper.IsOnline = true;
					listUserOnline.Add(datmapper);

				}
					
			
				return await Task.Run(() => listUserOnline.Where(x => x.Email != currentEmail).ToList());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<List<SellerDtoOutput>> GetUsersOnlineCustomerAsync( string[] userOnline)
		{
			try
			{
				var listUserOnline = new List<SellerDtoOutput>();


				foreach (var u in userOnline)
				{
					var seller = await context.Sellers.Where(s => s.Email == u).SingleOrDefaultAsync();
					var datmapper = mapper.Map<Seller, SellerDtoOutput>(seller);
					datmapper.IsOnline = true;
					listUserOnline.Add(datmapper);

				}


				return await Task.Run(() => listUserOnline.ToList());
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<List<SellerDtoOutput>> GetUsersOfflineAsync(string currentEmail, List<SellerDtoOutput> sellersOnline)
		{
			try
			{
				var listUserOffline = new List<SellerDtoOutput>();
				List<SellerDtoOutput> listseller = await context.Sellers.Where(x => x.IsBanned == false)
					.Select(s => mapper.Map<Seller, SellerDtoOutput>(s)).ToListAsync();


				var differentEmailSellers = listseller
		   .Where(s1 => !sellersOnline.Any(s2 => s2.Email == s1.Email))
		   .Union(sellersOnline.Where(s2 => !listseller.Any(s1 => s1.Email == s2.Email)))
		   .ToList();
				Console.WriteLine("List" + differentEmailSellers);
				return await Task.Run(() => differentEmailSellers.Where(x => x.Email != currentEmail).ToList());
			}
			catch (Exception)
			{
				return null;
			}
		}


		public async Task<List<SellerDtoOutput>> GetUsersOfflineCustomerAsync(List<SellerDtoOutput> sellersOnline)
		{
			try
			{
				var listUserOffline = new List<SellerDtoOutput>();
				List<SellerDtoOutput> listseller = await context.Sellers.Where(x => x.IsBanned == false)
					.Select(s => mapper.Map<Seller, SellerDtoOutput>(s)).ToListAsync();


				var differentEmailSellers = listseller
		   .Where(s1 => !sellersOnline.Any(s2 => s2.Email == s1.Email))
		   .Union(sellersOnline.Where(s2 => !listseller.Any(s1 => s1.Email == s2.Email)))
		   .ToList();
				Console.WriteLine("List" + differentEmailSellers);
				return await Task.Run(() => differentEmailSellers.ToList());
			}
			catch (Exception)
			{
				return null;
			}
		}
		//public async Task<List<SellerDtoOutput>> GetUsersOnlineAsync(string currentEmail, string[] userOnline)
		//{
		//	try
		//	{
		//		var listUserOnline = new List<SellerDtoOutput>();
		//		var listseller = await context.Sellers.Where(x => x.IsBanned == false).ToListAsync();
		//		var emailSet = new HashSet<string>(userOnline);

		//		foreach (var s in listseller)
		//		{
		//			foreach (var u in userOnline)
		//			{
		//				if (u == s.Email)
		//				{
		//					var datmapper = mapper.Map<Seller, SellerDtoOutput>(s);
		//					datmapper.IsOnline = true;
		//					listUserOnline.Add(datmapper);

		//				}
		//				else
		//				{
		//					var datmapper = mapper.Map<Seller, SellerDtoOutput>(s);
		//					listUserOnline.Add(datmapper);
		//					break;
		//				}
		//			}
		//		}
		//		HashSet<string> uniqueEmails = new HashSet<string>();
		//		List<SellerDtoOutput> uniqueList = new List<SellerDtoOutput>();

		//		foreach (var seller in listUserOnline)
		//		{
		//			if (!uniqueEmails.Contains(seller.Email))
		//			{
		//				uniqueEmails.Add(seller.Email);
		//				uniqueList.Add(seller);
		//			}
		//		}

		//		return await Task.Run(() => uniqueList.Where(x => x.Email != currentEmail).ToList());
		//	}
		//	catch (Exception)
		//	{
		//		return null;
		//	}
		//}

	}
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageSeller;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
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

				var user = context.Sellers
					.Include(s=>s.SellerLicenseImages)
					 .Select(p => new SellerDtoOutput
					 {
						 SellerId = p.SellerId,
						 //FirstName = p.FirstName,
						 //LastName = p.LastName,
						// Gender = p.Gender,
						 ConfirmedEmail = p.ConfirmedEmail,
						 PhoneNumber=p.PhoneNumber,
						// BirthDate = p.BirthDate,
						 Email = p.Email,
						 IsBanned = p.IsBanned,
						 IsVerified=p.IsVerified,
						 IsOnline=p.IsOnline,
						 ShopName=p.ShopName,
						 ShopAddress=p.ShopAddress,
						 Images = context.ProfileImages
					.Where(pi => pi.UserId == p.SellerId&&pi.IsReplaced==false)
				   .Select(pi => new ImageSellerOutputDto
				   {
					   ImageId = pi.ImageId,
					   UserId = pi.UserId,
					   Path = pi.Path,
					   IsReplaced = pi.IsReplaced
				   }
				   )
						
				 .ToList(),
	             ImagesL = p.SellerLicenseImages.ToList(),

					 }
					 
					 
					 )

					.ToList();
				var output = this.Output<ListSellerDtoOutput>(Constants.ResultCdSuccess);
				output.Sellers = user;
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






		public async Task<SellerMessageDtoOutput> GetSellersAsync(string email)
		{
			var user = await context.Sellers
				.FirstOrDefaultAsync(x => x.Email == email && x.IsVerified == true && x.IsBanned == false);
			
			var datmapper = mapper.Map<Seller, SellerMessageDtoOutput>(user);
			var img = await context.ProfileImages.Where(s => s.UserId == datmapper.SellerId && s.IsReplaced == false).FirstOrDefaultAsync();
			ImageFileConvert.ImageOutputDto? imageInfor = null;
			if (img == null)
			{
				return datmapper;
			
			}
			imageInfor = ImageFileConvert.ConvertFileToBase64(img.UserId, img.Path, 3);
			
			//	var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>(imageInfor);
			datmapper.Image = imageInfor.ImageBase64;
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
		public async Task<List<SellerMessageDtoOutput>> GetUsersOnlineAsync(string currentEmail, string[] userOnline)
		{
			try
			{
				var listUserOnline = new List<SellerMessageDtoOutput>();


				foreach (var u in userOnline)
				{
					var seller = await context.Sellers.Where(s=>s.Email== u&&s.IsBanned == false && s.IsVerified == true).SingleOrDefaultAsync();
					var datmapper = mapper.Map<Seller, SellerMessageDtoOutput>(seller);
					datmapper.IsOnline = true;
					var img = await context.ProfileImages.Where(s => s.UserId == datmapper.SellerId && s.IsReplaced == false).FirstOrDefaultAsync();
					ImageFileConvert.ImageOutputDto? imageInfor = null;
					if (img == null)
					{
						listUserOnline.Add(datmapper);
						break;
					}
					imageInfor = ImageFileConvert.ConvertFileToBase64(img.UserId, img.Path, 3);
					if (imageInfor == null)
						continue;
					//	var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>(imageInfor);
					datmapper.Image = imageInfor.ImageBase64;
					listUserOnline.Add(datmapper);

				}
					
			
				return await Task.Run(() => listUserOnline.Where(x => x.Email != currentEmail).ToList());
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<List<SellerMessageDtoOutput>> GetUsersOnlineCustomerAsync( string[] userOnline)
		{
			try
			{
				var listUserOnline = new List<SellerMessageDtoOutput>();


				foreach (var u in userOnline)
				{
					var seller = await context.Sellers.Where(s => s.Email == u&& s.IsBanned == false && s.IsVerified == true).SingleOrDefaultAsync();
					var datmapper = mapper.Map<Seller, SellerMessageDtoOutput>(seller);
					datmapper.IsOnline = true;
					var img= await context.ProfileImages.Where(s=>s.UserId== datmapper.SellerId&&s.IsReplaced==false).FirstOrDefaultAsync();
					ImageFileConvert.ImageOutputDto? imageInfor = null;
					if (img == null)
					{
						listUserOnline.Add(datmapper);
						break;
					}
					imageInfor = ImageFileConvert.ConvertFileToBase64(img.UserId, img.Path, 3);
					if (imageInfor == null)
						continue;
				//	var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>(imageInfor);
					datmapper.Image = imageInfor.ImageBase64;
				                 
					listUserOnline.Add(datmapper);

				}


				return await Task.Run(() => listUserOnline.ToList());
			}
			catch (Exception)
			{
				return null;
			}
		}
		public async Task<List<SellerMessageDtoOutput>> GetUsersOfflineAsync(string currentEmail, List<SellerMessageDtoOutput> sellersOnline)
		{
			try
			{
				var listUserOffline = new List<SellerMessageDtoOutput>();
				List<SellerMessageDtoOutput> listseller = await context.Sellers.Where(x => x.IsBanned == false && x.IsVerified == true)
					.Select(s => mapper.Map<Seller, SellerMessageDtoOutput>(s)).ToListAsync();


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


		public async Task<List<SellerMessageDtoOutput>> GetUsersOfflineCustomerAsync(List<SellerMessageDtoOutput> sellersOnline)
		{
			try
			{
				var listUserOffline = new List<SellerMessageDtoOutput>();
				List<SellerMessageDtoOutput> listseller = await context.Sellers.Where(x => x.IsBanned == false&& x.IsVerified == true)
					.Select(s => mapper.Map<Seller, SellerMessageDtoOutput>(s)).ToListAsync();


				var differentEmailSellers = listseller
		   .Where(s1 => !sellersOnline.Any(s2 => s2.Email == s1.Email))
		   .Union(sellersOnline.Where(s2 => !listseller.Any(s1 => s1.Email == s2.Email)))
		   .ToList();
				Console.WriteLine("List" + differentEmailSellers);

				foreach (var seller in differentEmailSellers)
				{
					var img = await context.ProfileImages.Where(s => s.UserId == seller.SellerId && s.IsReplaced == false).FirstOrDefaultAsync();
					if (img == null)
					{
						break;
					}
					ImageFileConvert.ImageOutputDto? imageInfor = null;
					imageInfor = ImageFileConvert.ConvertFileToBase64(img.UserId, img.Path, 3);
					if (imageInfor == null)
						continue;
					//	var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>(imageInfor);
					seller.Image = imageInfor.ImageBase64;
				}

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

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace HFS_BE.DAO.ShipperDao
{
    public class ShipperDao : BaseDao
    {
        public ShipperDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ShipperInforList GetShippersBySellerId(string sellerId)
        {
            // get shipper of shop
            // with condition: not ban and verified
            var data = context.Shippers
                .Where(s => s.ManageBy.Equals(sellerId) && s.Status == 1)
                .Select(s => mapper.Map<Shipper, ShipperInfor>(s))
                .ToList();
            var output = Output<ShipperInforList>(Constants.ResultCdSuccess);
            output.Shippers = data;
            return output;
        }
		public ShipperInforList GetShippersBySellerId(ShipperInforDtoInputbySellerId sellerId)
		{
			var data = context.Shippers
				.Where(s => s.ManageBy.Equals(sellerId.ManageBy))
				.Select(s => mapper.Map<Shipper, ShipperInfor>(s))
				.ToList();
			var output = Output<ShipperInforList>(Constants.ResultCdSuccess);
			output.Shippers = data;
			return output;
		}
		public Shipper? GetShipperByShipperIdAndSellerId(string sellerId, string shipperId)
        {
            return context.Shippers.FirstOrDefault(x => x.ShipperId.Equals(shipperId) && x.ManageBy.Equals(sellerId));
        }
		//public string? ShipperId { get; set; }
		//public string? ShipperName { get; set; }
		//public string? Gender { get; set; }
		//public DateTime? BirthDate { get; set; }
		//public string Email { get; set; } = null!;
		//public string? PhoneNumber { get; set; }
		//public string? Avatar { get; set; }
		//public string? ManageBy { get; set; }
		//public bool? ConfirmedEmail { get; set; }
		//public bool? IsBanned { get; set; }
		//public bool? IsVerified { get; set; }
		public ShipperInforListByAdmin GetShipperAll()//chua mapper
		{
			var data = context.Shippers
				.Select(s =>new ShipperInforByAdmin
				{
					ShipperId = s.ShipperId,
					ShipperName=s.FirstName+" "+s.LastName,
					Gender=s.Gender,
					BirthDate=s.BirthDate,
					Email=s.Email,
					PhoneNumber=s.PhoneNumber,
					ManageBy=s.ManageBy,
					ConfirmedEmail=s.ConfirmedEmail,
					IsPhoneVerified=s.IsPhoneVerified,
					Status=s.Status,
					CreateDate=s.CreateDate,
					Note=s.Note,
					IdcardNumber=s.IdcardNumber,
					IdcardFrontImage=s.IdcardFrontImage,
					IdcardBackImage=s.IdcardBackImage,
					Images= context.ProfileImages
					.Where(pi => pi.UserId == s.ShipperId && pi.IsReplaced == false)
				   .Select(pi => new ImageShipperOutputDto
				   {
					   ImageId = pi.ImageId,
					   UserId = pi.UserId,
					   Path = pi.Path,
					   IsReplaced = pi.IsReplaced
				   })
				 .ToList()
				}
				
					
					)
				.ToList();
			var output = Output<ShipperInforListByAdmin>(Constants.ResultCdSuccess);
			output.Shippers = data;
			return output;
		}

		//public BaseOutputDto BanShipper(BanShipperDtoInput input)
		//{
		//	var validationContext = new ValidationContext(input, serviceProvider: null, items: null);
		//	var validationResults = new List<ValidationResult>();
		//	bool isValid = Validator.TryValidateObject(input, validationContext, validationResults, validateAllProperties: true);
		//	if (!isValid)
		//	{
		//		string err = "";
		//		foreach (var item in validationResults)
		//		{
		//			err += item.ToString() + " ";
		//		}
		//		return this.Output<BaseOutputDto>(Constants.ResultCdFail, err);
		//	}
		//	try
		//	{
		//		ShipperBan ban = new ShipperBan();
		//		var user = this.context.Shippers.FirstOrDefault(s => s.ShipperId == input.ShipperId);
		//		if (user == null)
		//		{
		//			return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper is not in data ");
		//		}
		//		ban.ShipperId = user.ShipperId;
		//		ban.Reason = input.Reason;
		//		user.IsBanned = input.IsBanned;
		//		ban.CreateDate = DateTime.Now;
		//		context.Shippers.Update(user);
		//		context.ShipperBans.Add(ban);
		//		context.SaveChanges();
		//		var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

		//		return output;
		//	}
		//	catch (Exception)
		//	{
		//		return this.Output<BaseOutputDto>(Constants.ResultCdFail);
		//	}
		//}

		public async Task<BaseOutputDto> ActiveShipper(ActiveShipperDtoInput input)
		{
			try
			{
				var user = this.context.Shippers.FirstOrDefault(s => s.ShipperId == input.ShipperId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper is not in data ");
				}
				user.Status =(byte) input.Status;
				user.Note = input.Note;
				context.Shippers.Update(user);
				context.SaveChanges();
				if (input.Status == 1)
				{
					input.Note = input.Note + "\n Thân mến, HOLA Food.";
					await SendEmailAsync(user.Email, "Đăng ký người giao hàng thành công", input.Note);
				}
				if (input.Status == 2)
				{
					input.Note = input.Note + ".\n Nếu bạn đã thay đổi thông tin vui lòng trả lời email này. \n Thân mến, HOLA Food.";
					await SendEmailAsync(user.Email, "Chưa chấp nhận người giao hàng", input.Note);
				}
				var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		private async Task<bool> SendEmailAsync(string toEmail, string subject, string content)
		{
			try
			{
				string from = "holafoodfpt@gmail.com";
				string pass = "wqsq fqmv iwhu ablr";
				MailMessage mail = new MailMessage();
				SmtpClient smtp = new SmtpClient("smtp.gmail.com");

				mail.To.Add(toEmail);
				mail.From = new MailAddress(from);
				mail.Subject = subject;
				mail.Body = "HOLA FOOD:" + content;
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Credentials = new NetworkCredential(from, pass);
				await smtp.SendMailAsync(mail);
				return true;
			}
			catch
			{
				return false;
			}

		}
		//public ListHistoryBanShipper ListHistoryShipper(BanShipperHistoryDtoInput Id)
		//{
		//	try
		//	{

		//		var user = this.context.ShipperBans.Where(s => s.ShipperId == Id.ShipperId).ToList();

		//		var output = this.Output<ListHistoryBanShipper>(Constants.ResultCdSuccess);
		//		output.data = mapper.Map<List<ShipperBan>, List<BanHistoryShipperDtoOutput>>(user);



		//		return output;
		//	}
		//	catch (Exception)
		//	{
		//		return this.Output<ListHistoryBanShipper>(Constants.ResultCdFail);
		//	}
		//}


		public BaseOutputDto InvitationShipper(InvitationShipperEmailDtoInput input)//bên seller gửi lời mời đến shipper
		{
			try
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
				var data = context.Shippers.FirstOrDefault(s => s.Email == input.Email);
				var datacheckis = context.Shippers.FirstOrDefault(s => s.Email == input.Email && s.Status == 1);
				
				var datainv = context.Invitations.Include(s => s.Shipper).FirstOrDefault(s => s.Shipper.Email == input.Email && s.SellerId == input.SellerId && s.Accepted == 0);
				if (data.ManageBy != null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper has a manager");
				}
				if (datacheckis == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Please wait for the admin to approve the shipper");
				}
			
				if (datainv != null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You invited them to be the shipper");
				}
				Invitation inv = new Invitation();
				inv.ShipperId = data.ShipperId;
				inv.SellerId = input.SellerId;
				inv.Accepted = 0;
				context.Invitations.Add(inv);
				context.SaveChanges();

				var output = Output<BaseOutputDto>(Constants.ResultCdSuccess);

				return output;

			}catch(Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "No shipper uses this email in the system");

			}
		
		}
		public ListInvitationShipperDtoOutput ListInvitationShipper(ShipperInforDtoInputbySellerId input)//list ra nhưng lời mời seller gửi 
		{
			var data = context.Invitations.Include(s => s.Shipper).Where(s => s.SellerId == input.ManageBy).OrderByDescending(s => s.InvitationId).ToList();

			var output = Output<ListInvitationShipperDtoOutput>(Constants.ResultCdSuccess);
			output.data = mapper.Map<List<Invitation>, List<InvitationShipperDtoOutput>>(data); ;
			return output;
		}
		public ListInvitationShipperbyShipperDtoOutput ListInvitationShipperByShipper(ListInvitationShipperDtoInput input)//list ra nhưng lời mời seller gửi cho shipper bên shipepr
		{
			var data = context.Invitations.Include(s => s.Seller).Where(e =>e.ShipperId==input.ShipperId&&e.Accepted==0).OrderByDescending(s=>s.InvitationId).ToList();
			var output = Output<ListInvitationShipperbyShipperDtoOutput>(Constants.ResultCdSuccess);
			output.data = mapper.Map<List<Invitation>, List<InvitationSellerDtoOutput>>(data); ;
			return output;
		}

		public BaseOutputDto AcceptInvitationShipper(InvitationShipperDtoInput input)//bên shipper accept lời mời đó
		{
			var data = context.Shippers.FirstOrDefault(s => s.ManageBy == input.SellerId && s.ShipperId == input.ShipperId);
			var datainv = context.Invitations.FirstOrDefault(s => s.SellerId == input.SellerId && s.ShipperId == input.ShipperId&&s.Accepted==0);
			var datacheck = context.Shippers.FirstOrDefault(s => s.ShipperId == input.ShipperId&&s.ConfirmedEmail==true&&s.IsPhoneVerified==true);
			if (data != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper has a manager");
			}
			if (datacheck == null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Please verify all personal information to receive invitation");
			}
			if (datainv == null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You are not invited to be a shipper for them");
			}
			
			datainv.Accepted = input.Accepted;
			context.Invitations.Update(datainv);
			context.SaveChanges();
			if (datainv.Accepted == 1)
			{
				var shipper = context.Shippers.FirstOrDefault(s =>s.ShipperId == input.ShipperId);
				shipper.ManageBy = input.SellerId;
				context.Shippers.Update(shipper);
				context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess, "Accept successfully");
			}
			else
			{
			//	var output = Output<BaseOutputDto>(Constants.ResultCdSuccess, "Reject successfully");

				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Reject successfully");
			}
			
			
		}

		public BaseOutputDto KickShipper(KickShipperDtoInput input)//bên seller có thể kick shipper ra khỏi quán mình
		{
			var data = context.Shippers.FirstOrDefault(s => s.ManageBy == input.SellerId && s.ShipperId == input.ShipperId);
		

			if (data == null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "That shipper is not managed by you");
			}
			//var datainv = context.Invitations.FirstOrDefault(s => s.SellerId == input.SellerId && s.ShipperId == input.ShipperId);
			data.ManageBy = null;
			context.Shippers.Update(data);
			//context.Invitations.Remove(datainv);
			context.SaveChanges();
			var output = Output<BaseOutputDto>(Constants.ResultCdSuccess);

			return output;
		}

		public GetShipperInforOutputDto GetShipperInfor(string shipperId)
		{
			try
			{
				var infor = this.context.Shippers.FirstOrDefault(x => x.ShipperId.Equals(shipperId));
				var output = this.Output<GetShipperInforOutputDto>(Constants.ResultCdSuccess);
                if (infor != null)
				{
					output.ShipperName = infor.FirstName + " " + infor.LastName;
					output.ShipperPhone = infor.PhoneNumber;
				}
				return output;
			}
			catch (Exception)
			{
				return this.Output<GetShipperInforOutputDto>(Constants.ResultCdFail);
			}
		}
    }
}

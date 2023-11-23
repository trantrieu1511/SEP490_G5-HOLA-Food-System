using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
                .Where(s => s.ManageBy.Equals(sellerId) && s.IsVerified == true && s.IsBanned == false)
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

        public ShipperInforListByAdmin GetShipperAll()//chua mapper
		{
			var data = context.Shippers
				.Select(s => mapper.Map<Shipper, ShipperInforByAdmin>(s))
				.ToList();
			var output = Output<ShipperInforListByAdmin>(Constants.ResultCdSuccess);
			output.Shippers = data;
			return output;
		}

		public BaseOutputDto BanShipper(BanShipperDtoInput input)
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
				ShipperBan ban = new ShipperBan();
				var user = this.context.Shippers.FirstOrDefault(s => s.ShipperId == input.ShipperId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper is not in data ");
				}
				ban.ShipperId = user.ShipperId;
				ban.Reason = input.Reason;
				user.IsBanned = input.IsBanned;
				ban.CreateDate = DateTime.Now;
				context.Shippers.Update(user);
				context.ShipperBans.Add(ban);
				context.SaveChanges();
				var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public BaseOutputDto ActiveShipper(ActiveShipperDtoInput input)
		{
			try
			{
				var user = this.context.Shippers.FirstOrDefault(s => s.ShipperId == input.ShipperId);
				if (user == null)
				{
					return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper is not in data ");
				}
				user.IsVerified = input.IsVerified;
				context.Shippers.Update(user);
				context.SaveChanges();
				var output = this.Output<BaseOutputDto>(Constants.ResultCdSuccess);

				return output;
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public ListHistoryBanShipper ListHistoryShipper(BanShipperHistoryDtoInput Id)
		{
			try
			{

				var user = this.context.ShipperBans.Where(s => s.ShipperId == Id.ShipperId).ToList();

				var output = this.Output<ListHistoryBanShipper>(Constants.ResultCdSuccess);
				output.data = mapper.Map<List<ShipperBan>, List<BanHistoryShipperDtoOutput>>(user);



				return output;
			}
			catch (Exception)
			{
				return this.Output<ListHistoryBanShipper>(Constants.ResultCdFail);
			}
		}


		public BaseOutputDto InvitationShipper(InvitationShipperEmailDtoInput input)//bên seller gửi lời mời đến shipper
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
			var data = context.Shippers.FirstOrDefault(s => s.Email==input.Email);
			var datacheckis = context.Shippers.FirstOrDefault(s => s.Email == input.Email&&s.IsBanned==false&&s.IsVerified==true);
		
			var datainv = context.Invitations.Include(s => s.Shipper).FirstOrDefault(s => s.Shipper.Email == input.Email && s.SellerId == input.SellerId&&s.Accepted==0);
			if (data.ManageBy != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper has a manager");
			}
			if (datacheckis==null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Please wait for the admin to approve the shipper");
			}
				if (datainv != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "You invited them to be the shipper");
			}
				Invitation inv = new Invitation();
				inv.ShipperId =data.ShipperId;
				inv.SellerId = input.SellerId;
				inv.Accepted = 0;
				context.Invitations.Add(inv);
				context.SaveChanges();	

			var output = Output<BaseOutputDto>(Constants.ResultCdSuccess);
			
			return output;
		}
		public ListInvitationShipperDtoOutput ListInvitationShipper(ShipperInforDtoInputbySellerId input)//list ra nhưng lời mời seller gửi 
		{
			var data = context.Invitations.Include(s => s.Shipper).Where(s => s.SellerId == input.ManageBy).ToList();

			var output = Output<ListInvitationShipperDtoOutput>(Constants.ResultCdSuccess);
			output.data = mapper.Map<List<Invitation>, List<InvitationShipperDtoOutput>>(data); ;
			return output;
		}
		public ListInvitationShipperbyShipperDtoOutput ListInvitationShipperByShipper(ListInvitationShipperDtoInput input)//list ra nhưng lời mời seller gửi cho shipper bên shipepr
		{
			var data = context.Invitations.Include(s => s.Seller).Where(e =>e.ShipperId==input.ShipperId&&e.Accepted==0).ToList();
			var output = Output<ListInvitationShipperbyShipperDtoOutput>(Constants.ResultCdSuccess);
			output.data = mapper.Map<List<Invitation>, List<InvitationSellerDtoOutput>>(data); ;
			return output;
		}

		public BaseOutputDto AcceptInvitationShipper(InvitationShipperDtoInput input)//bên shipper accept lời mời đó
		{
			var data = context.Shippers.FirstOrDefault(s => s.ManageBy == input.SellerId && s.ShipperId == input.ShipperId);
			var datainv = context.Invitations.FirstOrDefault(s => s.SellerId == input.SellerId && s.ShipperId == input.ShipperId&&s.Accepted==0);
			if (data != null)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "Shipper has a manager");
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
			}
			
			var output = Output<BaseOutputDto>(Constants.ResultCdSuccess);

			return output;
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

	}
}

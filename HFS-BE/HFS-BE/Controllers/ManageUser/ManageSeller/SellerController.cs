using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;
using HFS_BE.BusinessLogic.ManageUser.ManageSeller;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageUser.ManageSeller
{

	public class SellerController : BaseController
	{
		public SellerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("users/listseller")]

		public ListSellerDtoOutput ListSeller()
		{
			try
			{
				var business = this.GetBusinessLogic<SellerBusinessLogic>();
				var output = business.ListSeller();
				return output;
			}
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}

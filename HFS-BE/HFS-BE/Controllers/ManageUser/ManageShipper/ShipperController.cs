using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageUser.ManageShipper
{

	public class ShipperController : BaseController
	{
		public ShipperController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
	}
}

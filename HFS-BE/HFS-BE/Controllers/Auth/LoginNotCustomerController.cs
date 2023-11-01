using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{

	public class LoginNotCustomerController : BaseController
	{
		public LoginNotCustomerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
	}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.SendOTP
{
	
	public class SendOTPInputDto
	{
		public string UserId { get; set; }
		public string phoneNumber { get; set; }
	}
	public class VerifyOTPInputDto
	{
		public string UserId { get; set; }
		public string phoneNumber { get; set; }

		public int otp { get; set; }
	}
}

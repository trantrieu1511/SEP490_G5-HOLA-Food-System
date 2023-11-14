using HFS_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace HFS_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		IConfiguration _configuration;
		private readonly string _accountSid= "ACcdd3d2e56f5f2a0142e7fde60d4773ea";
		private readonly string _authToken= "97612807fe63ae3633835b0b2747ca36";
		private readonly string _twilioPhoneNumber= "+17168004183";

		public TestController(IConfiguration configuration)
		{
			_configuration = configuration;
			TwilioClient.Init(_accountSid, _authToken);
		}

		[HttpPost]
		public async Task<IActionResult> SendAsync()
		{
			var phoneNumber = new Twilio.Types.PhoneNumber("+84974280518");
			var smsMessage = await MessageResource.CreateAsync(
				body: "LƯ ĐZ VÃI ",
				from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
				to: new Twilio.Types.PhoneNumber("+84974280518")
			);
		
			Console.WriteLine(smsMessage.Sid);
			return Ok();

		}
	}
}

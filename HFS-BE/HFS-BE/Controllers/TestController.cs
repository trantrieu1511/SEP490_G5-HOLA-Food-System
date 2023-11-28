using HFS_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
namespace HFS_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		//	IConfiguration _configuration;
		//	private readonly string _accountSid = "ACcdd3d2e56f5f2a0142e7fde60d4773ea";
		//	private readonly string _authToken = "97612807fe63ae3633835b0b2747ca36";
		//	private readonly string _twilioPhoneNumber = "+17168004183";
		//	private readonly HttpClient _httpClient;
		//	//private readonly HttpClient _httpClient;
		//	private readonly string _apiKey;
		//	private readonly string _apiToken;
		//	private readonly string _otpEndpoint;

		//	public TestController()
		//	{
		//		_httpClient = new HttpClient();
		//		_apiKey = "mtbi2w4hlendfpxa1igthcu5p6mzxf7k";
		//		_apiToken = "mpktanoshzf4c81e3bydjl76ixr9wugv";
		//		_otpEndpoint = "https://otp.dev/api/verify/";
		//	}

		//	[HttpPost]
		//	public async Task<IActionResult> SendSmsOtp(string phoneNumber)
		//	{
		//		// Tạo header Authorization
		//		string authValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_apiKey}:{_apiToken}"));
		//		_httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authValue);

		//		// Tạo body dạng form data
		//		var formData = new Dictionary<string, string>
		//		{
		//			{ "channel", "sms" },
		//			{ "phone_sms", phoneNumber },
		//			{ "success_redirect_url", "https://example.com/success" },
		//			{ "fail_redirect_url", "https://example.com/fail" },
		//			{ "callback_url", "https://example.com/callback" },
		//			{ "metadata", "{\"order_id\":\"xfdu48sfdjsdf\", \"agent_id\":2258}" },
		//			{ "captcha", "true" },
		//			{ "hide", "true" },
		//			{ "lang", "en" }
		//		};

		//		var content = new FormUrlEncodedContent(formData);

		//		// Gửi yêu cầu POST đến endpoint
		//		var response = await _httpClient.PostAsync(_otpEndpoint, content);

		//		// Xử lý phản hồi từ API
		//		if (response.IsSuccessStatusCode)
		//		{
		//			string responseContent = await response.Content.ReadAsStringAsync();
		//			return Ok("Yêu cầu gửi SMS OTP thành công!");
		//		}
		//		else
		//		{
		//			return BadRequest("Yêu cầu gửi SMS OTP thất bại!");
		//		}
		//	}
		//}
		//public TestController(IConfiguration configuration)
		//	{
		//		_configuration = configuration;
		//		TwilioClient.Init(_accountSid, _authToken);
		//	}

		//	[HttpPost]
		//	public async Task<IActionResult> SendAsync()
		//	{
		//		var phoneNumber = new Twilio.Types.PhoneNumber("+84974280518");
		//		var smsMessage = await MessageResource.CreateAsync(
		//			body: "LƯ ĐZ VÃI ",
		//			from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
		//			to: new Twilio.Types.PhoneNumber("+84974280518")
		//		);

		//		Console.WriteLine(smsMessage.Sid);
		//		return Ok();

		//	}
		//}
		private readonly HttpClient _httpClient;

		public TestController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://provinces.open-api.vn/api/");
		}

		[HttpGet]
		public async Task<IActionResult> GetProvinces()
		{
			try
			{
				var response = await _httpClient.GetAsync("?depth=1");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return Ok(content);
				}

				return StatusCode((int)response.StatusCode, response.ReasonPhrase);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[HttpGet("2")]
		public async Task<IActionResult> GetProvinces2()
		{
			try
			{
				var response = await _httpClient.GetAsync("?depth=2");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return Ok(content);
				}

				return StatusCode((int)response.StatusCode, response.ReasonPhrase);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
		[HttpGet("3")]
		public async Task<IActionResult> GetProvinces3()
		{
			try
			{
				var response = await _httpClient.GetAsync("?depth=3");

				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					return Ok(content);
				}

				return StatusCode((int)response.StatusCode, response.ReasonPhrase);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}
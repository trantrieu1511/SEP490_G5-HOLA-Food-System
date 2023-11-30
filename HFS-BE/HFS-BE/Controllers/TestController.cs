using CloudinaryDotNet;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
			private readonly string _accountSid = "AC07cc5d2950187dd5ba62b18cf58fa774";
		private readonly string _authToken = "c51baf9208fbcc323ae990b60f8ebcea";
		private readonly string _twilioPhoneNumber = "+17274751881";
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

	
	
	private readonly HttpClient _httpClient;

		public TestController(IHttpClientFactory httpClientFactory)
		{
			_httpClient = httpClientFactory.CreateClient();
			_httpClient.BaseAddress = new Uri("https://provinces.open-api.vn/api/");
			TwilioClient.Init(_accountSid, _authToken);
		}
		[HttpPost("/testotp")]
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
		[HttpPost("/users/province")]
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
		[HttpPost("/users/district")]
		public async Task<IActionResult> GetProvinces2(UrlAddress url)
		{
			try
			{
				var response = await _httpClient.GetAsync(url.Url);

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

		public class UrlAddress
		{
			public string Url { get; set; }

          } 
		[HttpPost("/users/ward")]
		public async Task<IActionResult> GetProvinces3(UrlAddress url)
		{
			try
			{
				var response = await _httpClient.GetAsync(url.Url);

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
		[HttpPost("/users/ward2")]
		public async Task<IActionResult> GetOrder()
		{
			try
			{
				using (SEP490_HFS_2Context context =new SEP490_HFS_2Context())
				{
					//var user = context.Orders.Include(s => s.OrderProgresses).
					//	Where(s => s.OrderProgresses.Where(p => p.Status == 5).Any())
					//	.ToList();

					var user = context.Customers.Include(s=>s.Orders).ThenInclude(s=>s.OrderProgresses)
						 .Select(p => new CustomerDtoOutput
						 {
							 CustomerId = p.CustomerId,
							 FirstName = p.FirstName,
							 LastName = p.LastName,
							 Gender = p.Gender,
							 PhoneNumber = p.PhoneNumber,
							 BirthDate = p.BirthDate,
							 Email = p.Email,
							 ConfirmedEmail = (p.ConfirmedEmail),
							 NumberOfViolations = p.NumberOfViolations,
							 Orders=p.Orders
						.Where(s => s.OrderProgresses.Where(s => s.Status == 5).Any())
								 .Select(s => new OrderCustomerOutputDto
								 {
									 OrderId = s.OrderId,
									 OrderDate = s.OrderDate,
									 Note = s.OrderProgresses.FirstOrDefault(s => s.Status == 5).Note,
									 ShipAddress = s.ShipAddress,
									 ShipperId = s.ShipperId
								 }).ToList(),
							 Images = context.ProfileImages
						.Where(pi => pi.UserId == p.CustomerId && pi.IsReplaced == false)
					   .Select(pi => new ImageCustomerOutputDto
					   {
						   ImageId = pi.ImageId,
						   UserId = pi.UserId,
						   Path = pi.Path,
						   IsReplaced = pi.IsReplaced
					   })
					 .ToList()

						 })

						.ToList();

					//var order = context
					//	.Orders
					//	.Include(s => s.OrderProgresses)
					//	.Where(s => s.OrderProgresses.Where(s => s.Status == 5&&s.CustomerId==p.CustomerId).Any())
					//			 .Select(s => new OrderCustomerOutputDto
					//			 {
					//				 OrderId = s.OrderId,
					//				 OrderDate = s.OrderDate,
					//				 Note = s.OrderProgresses.FirstOrDefault(s => s.Status == 5).Note,
					//				 ShipAddress = s.ShipAddress,
					//				 ShipperId = s.ShipperId
					//			 }).ToList(); ;
					return Ok(user);
				}
				

				
				//	return Ok(user);
				

				
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}


		[HttpPost("/users/testsdt")]
		public async Task<IActionResult> GetProvinces9(String phonenumber)
		{
			try
			{
				string ma;
				Random random = new Random();

				int randomso = random.Next(1000, 9999);
				JwtSecurityToken token = GenerateSecurityToken(randomso);
				using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
				{
					var user = context.Customers.FirstOrDefault(s => s.PhoneNumber == phonenumber);
					ma = new JwtSecurityTokenHandler().WriteToken(token);
				}
				var list = new
				{
					ma = ma,
					so = randomso
				};
				return Ok(list);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		private JwtSecurityToken GenerateSecurityToken(int acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();

			var authClaims = new List<Claim>
			{
				new Claim("OTP",acc.ToString()),
			};
			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddMinutes(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}

		[HttpPost("/users/checksdt")]
		public async Task<IActionResult> GetProvinces8(int m, string token)
		{
			bool result = false;
			var conf = new ConfigurationBuilder()
		   .SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json", true, true)
		.Build();
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(conf["JWT:Secret"]);

			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				string otp = jwtToken.Claims.First(c => c.Type == "OTP").Value;
				if (otp.Equals(m.ToString()))
				{
					result = true;
				}

				return Ok(result);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

	}
}
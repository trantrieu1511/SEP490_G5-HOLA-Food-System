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
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace HFS_BE.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TestController : ControllerBase
	{
		//	IConfiguration _configuration;
			private readonly string _accountSid = "AC07cc5d2950187dd5ba62b18cf58fa774";
		private readonly string _authToken = "31e539186dc5406e286c152f283e9e99";
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
		[HttpPost]
		public async Task<IActionResult> SendAsync()
		{
			try
			{
				string otp;
				Random random = new Random();
				int randomso = random.Next(1000, 9999);
				JwtSecurityToken token = GenerateSecurityToken(randomso, 5);

				using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
				{
					var user = context.Customers.FirstOrDefault(s => s.PhoneNumber == "0974280518");

					if (user == null)
					{
						return BadRequest();
					}

					otp = new JwtSecurityTokenHandler().WriteToken(token);
					user.OtpToken = otp;
					context.SaveChanges();
				}

				var phoneNumber = new Twilio.Types.PhoneNumber("+84974280518");

				var smsMessage = await MessageResource.CreateAsync(
					body: "OTP:" + randomso.ToString(),
					from: new Twilio.Types.PhoneNumber(_twilioPhoneNumber),
					to: phoneNumber
				);

				Console.WriteLine($"Twilio Message SID: {smsMessage.Sid}");
				return Ok();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return StatusCode(500, "Internal Server Error");
			}
		}


		[HttpPost("/users/checkotp2")]
		public async Task<IActionResult> GetProvinces8(int otp,string phonenumber)
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
				using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
				{
					var user = context.Customers.FirstOrDefault(s => s.PhoneNumber == phonenumber);
					tokenHandler.ValidateToken(user.OtpToken, new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false,
						ClockSkew = TimeSpan.Zero
					}, out SecurityToken validatedToken);

					var jwtToken = (JwtSecurityToken)validatedToken;
					string otpdata = jwtToken.Claims.First(c => c.Type == "OTP").Value;
					if (otpdata.Equals(otp.ToString()))
					{
						result = true;
					}
					if (result)
					{
						user.IsPhoneVerified = true;
						context.SaveChanges();
					}

					return Ok(result);


				}

				
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
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
				JwtSecurityToken token = GenerateSecurityToken(randomso,1);
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

		private JwtSecurityToken GenerateSecurityToken(int acc,int timeexp)
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
					expires: DateTime.Now.AddMinutes(timeexp),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		[HttpPost("/users/map")]
		public async Task<IActionResult> GetProvinces8(GetMap get)
		{
			string key = "";
			var encodedAddress = System.Uri.EscapeDataString(get.address);
			var apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}&key={key}";

			var response = await _httpClient.GetStringAsync(apiUrl);

			return Content(response, "application/json");
		}

		public class GetMap
		{
			public string address { get; set; }
		}
		[HttpGet]
		public async Task<IActionResult> GetDirections([FromQuery] string origin, [FromQuery] string destination, [FromQuery] string apiKey)
		{
			 apiKey = "";
			var directions = await GetDirectionsFromGoogleMapsAsync(origin, destination, apiKey);
			return Ok(directions);
		}

		private async Task<string> GetDirectionsFromGoogleMapsAsync(string origin, string destination, string apiKey)
		{
			
				var apiUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={origin}&destination={destination}&key={apiKey}";
				var response = await _httpClient.GetStringAsync(apiUrl);
				return response;
			
		}
		public class GetMap2
		{
			public string origin { get; set; }
			public string destination { get; set; }

		}
	}
}
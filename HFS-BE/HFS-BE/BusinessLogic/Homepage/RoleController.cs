using AutoMapper;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Web;

namespace HFS_BE.Controllers.Homepage
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{

		IConfiguration _configuration;
		private const string MailgunApiBaseUrl = "https://api.mailgun.net/v3/";
		private const string Sercet = "9e72bbde8bdf0ef145bc1fdb95ff5845";
		private const string ApiKey = "426b426ae3a1120f6f5cdc60e879aafc";

		public RoleController(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		[HttpGet]
		[Authorize]
		public IActionResult Get()
		{
			try
			{
				using (SEP490_HFSContext context = new SEP490_HFSContext())
				{
					var data = context.Roles.ToList();
					if (data == null)
					{
						return NotFound();
					}
					return Ok(data);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}
		[HttpPost]
		public IActionResult RequiredToken(AuthDaoInputDto request)
		{
			using (SEP490_HFSContext context = new SEP490_HFSContext())
			{
				//Include(s => s.Customers)
				HFS_BE.Models.User acc = context.Users.FirstOrDefault(u => u.Email == request.Email);
				if (acc == null)
				{
					return Unauthorized("Username or Password incorrect!");
				}
				bool check = CheckPassword(request.Password, acc);
				if (check == false)
				{
					return Unauthorized("Username or Password incorrect!");
				}
				JwtSecurityToken token = GenerateSecurityToken(acc);

				return Ok(new TokenResponse
				{
					AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
					User = acc
				});
				//return Ok(acc);
			}

		}

		private bool CheckPassword(string password, HFS_BE.Models.User user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}



		private JwtSecurityToken GenerateSecurityToken(HFS_BE.Models.User acc)
		{
			string role = acc.RoleId.ToString();

			var authClaims = new List<Claim>
			{
					new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Role, role),
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: _configuration["JWT:ValidIssuer"],
					audience: _configuration["JWT:ValidAudience"],
					expires: DateTime.Now.AddDays(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}
		[HttpPost("confirmation")]
		public async Task<IActionResult> SendConfirmationEmail([FromBody] string toEmail)
		{
			string confirmationCode = GenerateConfirmationCode(toEmail);


			string subject = "Xác nhận thay đổi trạng thái";
			string message = $"Vui lòng nhấp vào liên kết sau để xác nhận thay đổi trạng thái: {GetConfirmationLink("1", confirmationCode)}";

			try
			{
				await SendEmail(toEmail, subject, message);
				return Ok();
			}
			catch (Exception ex)
			{
				// Xử lý lỗi gửi email
				return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
			}
		}
		private string GenerateConfirmationCode(string userId)//tạo ra mã đễ 
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("userId", userId) }),
				Expires = DateTime.UtcNow.AddMinutes(10),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var confirmationCode = tokenHandler.WriteToken(token);

			return confirmationCode;
		}
		private bool ValidateConfirmationCode(string confirmationCode, out string userId)
		{
			userId = null;

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

			try
			{
				tokenHandler.ValidateToken(confirmationCode, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				userId = jwtToken.Claims.First(c => c.Type == "userId").Value;

				return true;
			}
			catch
			{
				return false;
			}
		}
		public class TokenResponse
		{
			public string AccessToken { get; set; }
			public HFS_BE.Models.User User { get; set; }
		}

		private string GetConfirmationLink(string userId, string confirmationCode)
		{
			string baseUrl = "https://example.com/confirm";
			var query = new Dictionary<string, string>
		{
			{ "userId", userId },
			{ "code", confirmationCode }
		};
			var confirmationLink = QueryHelpers.AddQueryString(baseUrl, query);
			return confirmationLink;
		}


		private async Task SendEmail(string toEmail, string subject, string message)
		{
			var httpClient = new HttpClient();

			var apiKey = "426b426ae3a1120f6f5cdc60e879aafc";
			var apiSecret = "9e72bbde8bdf0ef145bc1fdb95ff5845";
			var client = new MailjetClient("426b426ae3a1120f6f5cdc60e879aafc", "9e72bbde8bdf0ef145bc1fdb95ff5845");
			var request = new MailjetRequest
			{
				Resource = Send.Resource,
			}
			.Property(Send.Messages, new JArray {
	new JObject {
		{"FromEmail ", new JObject {
			{"Email", "lunguyen2k18@gmail.com"},
			{"Name", "lu nguyen"}
		}},
		{"To", new JArray {
			new JObject {
				{"Email", toEmail},
				{"Name", "lu2"}
			}
		}},
		{"Subject", "Your email subject"},
		{"TextPart", "Your email content"}
	}
			});

			var response = await client.PostAsync(request);
			if (response.IsSuccessStatusCode)
			{
				throw new Exception("thanh cong.");
			}
			else
			{
				throw new Exception(response.Content.ToString());
			}
		}
		[HttpPost("okdi")]
		public async Task<IActionResult> SendMail(string toEmail ,string content)
		{
			try
			{
				string from = "holafoodfpt@gmail.com";
				string pass = "holafoodfpt123";
				MailMessage mail = new MailMessage();
				SmtpClient smtp = new SmtpClient("smtp.gmail.com");
				Random r = new Random();
				int random = r.Next(1000, 9999);
				mail.To.Add(toEmail);
				mail.From = new MailAddress(from);
				mail.Subject = "PRN221";
				mail.Body = "Code:" + random;
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Credentials = new NetworkCredential(from, pass);
				await smtp.SendMailAsync(mail);
				return Ok();
			}catch(Exception ex)
			{
				return BadRequest();
			}
			
		}
	}
}





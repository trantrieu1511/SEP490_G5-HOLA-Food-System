using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
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
using HFS_BE.Utils;
using Microsoft.OpenApi.Writers;

namespace HFS_BE.Controllers.SendOTP
{

	public class SendOTPController : BaseController
	{

		IConfiguration _configuration;
		//private readonly string _accountSid = "AC07cc5d2950187dd5ba62b18cf58fa774";
		//private readonly string _authToken = "31e539186dc5406e286c152f283e9e99";
		//private readonly string _accountSid = "ACa94af58875970c9219b70cb061ffac31";
	//	private readonly string _authToken = "43aa1d745bc950d57601d73f26ced8e9";
		

		//private readonly string _twilioPhoneNumber = "+17274751881"; // Lu
		//private readonly string _twilioPhoneNumber = "+18177179100"; // Trieu
		public SendOTPController(SEP490_HFS_2Context context, IMapper mapper, IConfiguration configuration) : base(context, mapper)
		{
			_configuration = configuration;
			TwilioClient.Init(_configuration["Twilio:accountSid"], _configuration["Twilio:authToken"]);
			
		}
		[HttpPost("users/sendotp")]
		public async Task<BaseOutputDto> SendAsync(SendOTPInputDto inputDto)
		{
			try
			{
				string otp;
				Random random = new Random();
				int randomso = random.Next(1000, 9999);
				Console.WriteLine(randomso);
				JwtSecurityToken token = GenerateSecurityToken(randomso, 2);

				using (SEP490_HFS_2Context context = new SEP490_HFS_2Context())
				{
					
					if (inputDto.UserId.Substring(0, 2).Equals("CU"))
					{
						var user = context.Customers.FirstOrDefault(s => s.PhoneNumber == inputDto.phoneNumber);
						if (user == null)
						{
							return this.Output<BaseOutputDto>(Constants.ResultCdFail);
						}
						otp = new JwtSecurityTokenHandler().WriteToken(token);
						user.OtpToken = otp;
						user.OtpTokenExpiryTime = 5;
						context.SaveChanges();
					}
					else if(inputDto.UserId.Substring(0, 2).Equals("SE"))
					{
						var seller = context.Sellers.FirstOrDefault(s => s.PhoneNumber == inputDto.phoneNumber);
						if (seller == null)
						{
							return this.Output<BaseOutputDto>(Constants.ResultCdFail);
						}
						otp = new JwtSecurityTokenHandler().WriteToken(token);
						seller.OtpToken = otp;
						seller.OtpTokenExpiryTime = 5;
						context.SaveChanges();

					}
					else if (inputDto.UserId.Substring(0, 2).Equals("SH"))
					{
						var shipper = context.Shippers.FirstOrDefault(s => s.PhoneNumber == inputDto.phoneNumber);
						if (shipper == null)
						{
							return this.Output<BaseOutputDto>(Constants.ResultCdFail);
						}
						otp = new JwtSecurityTokenHandler().WriteToken(token);
						shipper.OtpToken = otp;
						shipper.OtpTokenExpiryTime = 5;
						context.SaveChanges();
					}
					
					
				}

				var phoneNumber = new Twilio.Types.PhoneNumber("+84868342491");

				var smsMessage = await MessageResource.CreateAsync(
					body: "HOLAFOOD OTP:" + randomso.ToString(),
					from: new Twilio.Types.PhoneNumber(_configuration["Twilio:twilioPhoneNumber"]),
					to: phoneNumber
				);

				//Console.WriteLine($"Twilio Message SID: {smsMessage.Sid}");
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex.Message}");
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		private JwtSecurityToken GenerateSecurityToken(int acc, int timeexp)
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
					//expires: DateTime.Now.AddSeconds(timeexp),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}

		[HttpPost("users/checkotp")]
		public async Task<BaseOutputDto> CheckOTP(VerifyOTPInputDto inputDto)
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
					if (inputDto.UserId.Substring(0, 2).Equals("CU"))
					{
						var user = context.Customers.FirstOrDefault(s => s.PhoneNumber == inputDto.phoneNumber);
						if (user == null)
						{
							return this.Output<BaseOutputDto>(Constants.ResultCdFail);
						}
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
						if (otpdata.Equals(inputDto.otp.ToString()))
						{
							result = true;
						}
						else
						{
							return Output<BaseOutputDto>(Constants.ResultCdFail, "Incorrect Otp.");
						}
					
						if (result)
						{
							user.IsPhoneVerified = true;
							context.SaveChanges();
						}
					}else if(inputDto.UserId.Substring(0, 2).Equals("SE"))
					{
						var user = context.Sellers.FirstOrDefault(s => s.PhoneNumber == inputDto.phoneNumber);
						if (user == null)
						{
							return this.Output<BaseOutputDto>(Constants.ResultCdFail);
						}
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
						if (otpdata.Equals(inputDto.otp.ToString()))
						{
							result = true;
                        }
                        else
                        {
                            return Output<BaseOutputDto>(Constants.ResultCdFail, "Incorrect Otp.");
                        }

                        if (result)
						{
							user.IsPhoneVerified = true;
							context.SaveChanges();
						}


					}
					else if (inputDto.UserId.Substring(0, 2).Equals("SH"))
					{
						var user = context.Shippers.FirstOrDefault(s => s.PhoneNumber == inputDto.phoneNumber);
						if (user == null)
						{
							return this.Output<BaseOutputDto>(Constants.ResultCdFail);
						}
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
						if (otpdata.Equals(inputDto.otp.ToString()))
						{
							result = true;
                        }
                        else
                        {
                            return Output<BaseOutputDto>(Constants.ResultCdFail, "Incorrect Otp.");
                        }

                        if (result)
						{
							user.IsPhoneVerified = true;
							context.SaveChanges();
						}


					}


					return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);


				}


			}
			catch (Exception ex)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail, "OTP is expired");
			}
		}
	}
}

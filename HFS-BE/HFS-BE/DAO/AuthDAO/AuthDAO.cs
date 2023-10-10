using AutoMapper;
using Google.Apis.Auth;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Ultis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace HFS_BE.Dao.AuthDao
{
	public class AuthDao : BaseDao
	{
		public AuthDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public AuthDaoOutputDto Login(AuthDaoInputDto input)
		{
            var output = new AuthDaoOutputDto();
			var user = context.Users.Where(s=>s.Email==input.Email).FirstOrDefault();
			if (user == null)
			{
				 return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Username Or Password Was Invalid");
			}
		
			var match = CheckPassword(input.Password, (User)user);

			if (!match)
			{
				 return this.Output<AuthDaoOutputDto>(Constants.ResultCdFail, "Username Or Password Was Invalid");
			}
			JwtSecurityToken token = GenerateSecurityToken((User)user);
			output.Token = new JwtSecurityTokenHandler().WriteToken(token);
			return output;

		}
		private bool CheckPassword(string password, User user)
		{
			bool result;

			using (HMACSHA256? hmac = new HMACSHA256(user.PasswordSalt))
			{
				var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				result = compute.SequenceEqual(user.PasswordHash);
			}

			return result;
		}

		//private dynamic GenerateSecurityToken(User user)
		//{
		//	var conf = new ConfigurationBuilder()
		//		.SetBasePath(Directory.GetCurrentDirectory())
		//		.AddJsonFile("appsettings.json", true, true)
		//		.Build();

		//	var tokenHandler = new JwtSecurityTokenHandler();
		//	var key = Encoding.ASCII.GetBytes(conf["JWT:Secret"]);



		//	var authSigningKey = new SymmetricSecurityKey(key);
		//	var tokenDescriptor = new SecurityTokenDescriptor
		//	{
		//		Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.Role, user.RoleId.ToString()),
		//				}),
		//		Expires = DateTime.UtcNow.AddDays(7),
		//		SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		//	};
		//	var token = tokenHandler.CreateToken(tokenDescriptor);
		//	var encrypterToken = tokenHandler.WriteToken(token);


		//	return new { token = encrypterToken, username = user.Email };
		//}

		private JwtSecurityToken GenerateSecurityToken(User acc)
		{
			var conf = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();
			string role = acc.RoleId.ToString();

			var authClaims = new List<Claim>
			{
					new Claim(ClaimTypes.Email, acc.Email),
				new Claim(ClaimTypes.Role, role),
			};

			var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(conf["JWT:Secret"]));

			var token = new JwtSecurityToken(issuer: conf["JWT:ValidIssuer"],
					audience: conf["JWT:ValidAudience"],
					expires: DateTime.Now.AddDays(1),
					claims: authClaims,
					signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)); ;

			return token;
		}

		public BaseOutputDto Register(RegisterDto model)
		{
			var user = new User { Email = model.Email, RoleId = model.RoleId, BirthDate = model.BirthDate, FirstName = model.FirstName, LastName = model.LastName, Gender = "Male" };


			using (HMACSHA256? hmac = new HMACSHA256())
			{
				user.PasswordSalt = hmac.Key;
				user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(model.Password));
			}

			try
				{
					context.Users.Add(user);
					context.SaveChanges();
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
				catch (Exception ex)
				{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public async Task<AuthOutputDto>? LoginWithGoogleAsync(string credential)
		{
			var conf = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", true, true)
				.Build();

			var output = new AuthOutputDto();
			var settings = new GoogleJsonWebSignature.ValidationSettings()
			{
				Audience = new List<string> { (conf["ApplicationSettings:GoogleClientId"]) }
			};

			var payload = await GoogleJsonWebSignature.ValidateAsync(credential, settings);

			var user = context.Users.Where(x => x.Email == payload.Email).FirstOrDefault();

			if (user == null)
			{
				var user1 = new User { Email = payload.Email, RoleId = 1, FirstName = payload.Name, LastName = payload.Name, Gender = "Male" };

				using (HMACSHA256? hmac = new HMACSHA256())
				{
					user1.PasswordSalt = hmac.Key;
					user1.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload.Email));
				}
				try
				{
					context.Users.Add(user1);
					context.SaveChanges();
					JwtSecurityToken token = GenerateSecurityToken((User)user);
					output.Token = new JwtSecurityTokenHandler().WriteToken(token);
					return output;
				}
				catch (Exception ex)
				{
					return this.Output<AuthOutputDto>(Constants.ResultCdFail);
				}
			}

			else
			{
				return output;
			}

			
		}

	}
}

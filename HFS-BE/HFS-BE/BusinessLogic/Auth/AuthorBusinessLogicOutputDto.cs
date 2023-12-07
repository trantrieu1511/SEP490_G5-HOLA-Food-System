using HFS_BE.Base;
using HFS_BE.Services;

namespace HFS_BE.BusinessLogic.Auth
{
	public class LoginOutputDto:BaseOutputDto
	{ 
		public string Token { get; set; }
		public RefreshToken RefreshToken { get; set; }
	}
}

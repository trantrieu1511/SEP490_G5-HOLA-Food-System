using System.Security.Claims;

namespace HFS_BE.Hubs.Extensions
{
	public static class ClaimsPrincipleExtensions
	{
		public static string GetEmail(this ClaimsPrincipal user)
		{
			return user.FindFirst(ClaimTypes.Email )?.Value;
		}
	}
}

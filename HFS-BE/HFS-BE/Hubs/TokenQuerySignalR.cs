using Microsoft.AspNetCore.SignalR;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HFS_BE.Hubs
{
    public class TokenQuerySignalR
    {
        private static readonly TokenQuerySignalR _singleton = new TokenQuerySignalR();

        private TokenQuerySignalR() { }

        public static TokenQuerySignalR GetSingleton()
        {
            return _singleton;
        }

        public string? GetUserId(HubCallerContext context)
        {
            var userid = context.User?.FindFirst("userId")?.Value!;
            
            /*var httpContext = context.GetHttpContext();
            if (httpContext != null)
            {
                var jwtToken = httpContext.Request.Query["access_token"];
                var handler = new JwtSecurityTokenHandler();
                if (!string.IsNullOrEmpty(jwtToken))
                {
                    var token = handler.ReadJwtToken(jwtToken);
                    var tokenS = token as JwtSecurityToken;

                    // replace email with your claim name
                    return tokenS.Claims.First(claim => claim.Type == "userId").Value;
                    
                }
            }*/

            return userid;
        }
    }
}

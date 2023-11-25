using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HFS_BE.Utils
{
    public class JwtExpirationAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var requestPath = context.HttpContext.Request.Path.Value;
            if (requestPath.Equals("/auths/refresh"))
                return;

            var expirationClaim = context.HttpContext.User.FindFirst("exp");
            if (expirationClaim != null && long.TryParse(expirationClaim.Value, out var expirationTime))
            {
                var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationTime).UtcDateTime;
                if (expirationDate < DateTime.UtcNow)
                {
                    // Token has expired, return unauthorized
                    context.HttpContext.Response.StatusCode = 401;
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }
}

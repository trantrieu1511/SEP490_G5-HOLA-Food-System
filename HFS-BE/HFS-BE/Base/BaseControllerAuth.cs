using AutoMapper;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HFS_BE.Base
{
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(JwtExpirationAuthorizationFilter))]
    public class BaseControllerAuth : ControllerBase
    {
        private readonly SEP490_HFS_2Context context;
        public readonly IMapper mapper;
        protected readonly ITokenService _tokenService;

        public BaseControllerAuth(SEP490_HFS_2Context context, IMapper mapper, ITokenService tokenService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            //this.context.Database.BeginTransaction();
        }


        public T GetBusinessLogic<T>() where T : BaseBusinessLogicAuth
        {

            return (T)Activator.CreateInstance(typeof(T), context, mapper, _tokenService);
        }

        [NonAction]
        public T Output<T>(bool success) where T : BaseOutputDto, new()
        {
            //    if (!success) this.context.Database.RollbackTransaction();
            return new T
            {
                Message = success ? "Success" : "Fail",
                Success = success,
                Errors = null
            };
        }

        [NonAction]
        public T Output<T>(bool success, string content) where T : BaseOutputDto, new()
        {
            //if (!success) this.context.Database.RollbackTransaction();
            return new T
            {
                Message = success ? "Success" : content,
                Success = success,
                Errors = null
            };
        }

        [NonAction]
        public string GetAccessRight()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var a = identity.FindFirst(ClaimTypes.Role)?.Value;
            return a;
        }


        [NonAction]
        public UserDto? GetUserInfor()
        {
            if (User.Identity is ClaimsIdentity identity)
            {
                var email = identity.FindFirst(ClaimTypes.Email)?.Value;
                var roleId = identity.FindFirst(ClaimTypes.Role)?.Value;
                var name = identity.FindFirst(ClaimTypes.Name)?.Value;
                var userid = identity.FindFirst("userId")?.Value;
                return new UserDto { Email = email, Name = name, UserId = userid, Role = roleId };
            }
            return null;
        }
    }
}

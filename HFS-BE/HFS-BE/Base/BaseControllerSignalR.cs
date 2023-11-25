using AutoMapper;
using HFS_BE.Hubs;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HFS_BE.Base
{
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    [ServiceFilter(typeof(JwtExpirationAuthorizationFilter))]
    public class BaseControllerSignalR : ControllerBase
    {
        private readonly SEP490_HFS_2Context context;
        public readonly IMapper mapper;
        protected readonly IHubContextFactory _hubContextFactory;

        public BaseControllerSignalR(SEP490_HFS_2Context context, IMapper mapper, IHubContextFactory hubContextFactory)
        {
            this.context = context;
            this.mapper = mapper;
            _hubContextFactory = hubContextFactory;
            //this.context.Database.BeginTransaction();
        }


        public T GetBusinessLogic<T>() where T : BaseBusinessLogic
        {

            return (T)Activator.CreateInstance(typeof(T), context, mapper);
        }

        [NonAction]
        public T Output<T>(bool success) where T : BaseOutputDto, new()
        {
            //if (!success) this.context.Database.RollbackTransaction();
            //else context.Database.CommitTransaction();
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
            //else context.Database.CommitTransaction();
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

﻿using AutoMapper;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HFS_BE.Base
{
    [ApiController]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public class BaseController : ControllerBase
    {
        private readonly SEP490_HFSContext context;
        public readonly IMapper mapper;

        public BaseController(SEP490_HFSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [NonAction]
        public T GetBusinessLogic<T>() where T : BaseBusinessLogic
        {

            return (T)Activator.CreateInstance(typeof(T), context, mapper);
        }

        [NonAction]
        public T Output<T>(bool success) where T : BaseOutputDto, new()
        {
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
            return new T
            {
                Message = success ? "Success" : content,
                Success = success,
                Errors = null
            };
        }

        [NonAction]
        public int GetAccessRight()
        {
            ClaimsIdentity identity = (ClaimsIdentity)User.Identity;
            var a = identity.FindFirst(ClaimTypes.Role)?.Value;
            return Convert.ToInt16(a);
        }


        [NonAction]
        public UserDto? GetUserInfor()
        {
            if (User.Identity is ClaimsIdentity identity)
            {
                var email = identity.FindFirst(ClaimTypes.Email)?.Value;
                var roleId = identity.FindFirst(ClaimTypes.Role)?.Value;
                var name = identity.FindFirst(ClaimTypes.Name)?.Value;
                return new UserDto { Email = email, RoleId = Convert.ToInt16(roleId), Name = name };
            }
            return null;
        }
    }
}

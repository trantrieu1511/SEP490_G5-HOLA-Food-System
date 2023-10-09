using AutoMapper;
using HFS_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;

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

        public T GetBusinessLogic<T>() where T : BaseBusinessLogic
        {

            return (T)Activator.CreateInstance(typeof(T), context, mapper);
        }

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
        public string GetAccessRight()
        {
            return "user";
        }
    }
}

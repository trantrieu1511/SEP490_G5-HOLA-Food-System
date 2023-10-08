using AutoMapper;
using HFS_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Base
{
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

        public BaseOutputDto Output(bool success)
        {
            var output = new BaseOutputDto()
            {
                Errors = null,
                Message = success ? "Success" : "Fail",
                Success = success,
            };
            return output;
        }
    }
}

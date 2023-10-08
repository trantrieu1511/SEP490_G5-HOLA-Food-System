using AutoMapper;
using HFS_BE.Models;

namespace HFS_BE.Base
{
    public class BaseDao
    {
        public readonly SEP490_HFSContext context;
        public readonly IMapper mapper;
        public BaseDao(SEP490_HFSContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
    }
}

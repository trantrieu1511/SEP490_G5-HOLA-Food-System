using AutoMapper;
using HFS_BE.Models;

namespace HFS_BE.Base
{
    public class BaseBusinessLogic
    {
        private readonly SEP490_HFSContext context;
        public readonly IMapper mapper;
        public BaseBusinessLogic(SEP490_HFSContext context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }
        public T CreateDao<T>() where T : BaseDao
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

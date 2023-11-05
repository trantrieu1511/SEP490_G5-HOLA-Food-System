using AutoMapper;
using HFS_BE.Models;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Base
{
    public class BaseBusinessLogicSignalR
    {
        private readonly SEP490_HFS_2Context context;
        public readonly IMapper mapper;
        protected readonly IHubContext<Hub> hubContext;

        public BaseBusinessLogicSignalR(SEP490_HFS_2Context context, IMapper mapper, IHubContext<Hub> hubContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.hubContext = hubContext;
        }
        public T CreateDao<T>() where T : BaseDao
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

        public T Output<T>(bool success, string content) where T : BaseOutputDto, new()
        {
            return new T
            {
                Message = content,
                Success = success,
                Errors = null
            };
        }

        public T Output<T>(bool success, string content, ICollection<string> errors) where T : BaseOutputDto, new()
        {
            return new T
            {
                Message = content,
                Success = success,
                Errors = new ErrorsMessage
                {
                    SystemErrors = errors
                }
            };
        }

        public T Output<T>(bool success, string content, string error) where T : BaseOutputDto, new()
        {
            var errors = new List<string>();
            errors.Add(error);
            return new T
            {
                Message = content,
                Success = success,
                Errors = new ErrorsMessage
                {
                    SystemErrors = errors
                }
            };
        }
    }
}

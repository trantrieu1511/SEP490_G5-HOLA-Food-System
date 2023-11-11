using AutoMapper;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR;

namespace HFS_BE.Base
{
    public class BaseBusinessLogic
    {
        private readonly SEP490_HFS_2Context context;
        public readonly IMapper mapper;

        public BaseBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) {
            this.context = context;
            this.mapper = mapper;
        }
        public T CreateDao<T>() where T : BaseDao
        {
            return (T)Activator.CreateInstance(typeof(T), context, mapper);
        }


        public T Output<T>(bool success) where T : BaseOutputDto, new()
        {
            if (!success) this.context.Database.RollbackTransaction();
            return new T
            {
                Message = success ? "Success" : "Fail",
                Success = success,
                Errors = null
            };
         }

        public T Output<T>(bool success, string content) where T : BaseOutputDto, new()
        {
            if (!success) this.context.Database.RollbackTransaction();
            return new T
            {
                Message = content,
                Success = success,
                Errors = null
            };
        }

        public T Output<T>(bool success, string content, ICollection<string> errors) where T : BaseOutputDto, new()
        {
            if (!success) this.context.Database.RollbackTransaction();
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
            if (!success) this.context.Database.RollbackTransaction();
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

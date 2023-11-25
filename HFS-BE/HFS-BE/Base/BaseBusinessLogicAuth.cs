using AutoMapper;
using HFS_BE.Models;
using HFS_BE.Services;

namespace HFS_BE.Base
{
    public class BaseBusinessLogicAuth
    {
        private readonly SEP490_HFS_2Context context;
        public readonly IMapper mapper;
        protected readonly ITokenService _tokenService;

        public BaseBusinessLogicAuth(SEP490_HFS_2Context context, IMapper mapper, ITokenService tokenService)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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

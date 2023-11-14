using AutoMapper;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.Base
{
    public class BaseDao
    {
        public readonly SEP490_HFS_2Context context;
        public readonly IMapper mapper;
        public BaseDao(SEP490_HFS_2Context context, IMapper mapper)
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

        public T Output<T>(bool success, string content) where T : BaseOutputDto, new()
        {
            
            return new T
            {
                Message = success ? "Success" : content,
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

        public List<T> Paginate<T>(List<T> sourceList, PaginationDto pagination)
        {
            return sourceList.Skip((pagination.pageNumber - 1) * pagination.pageSize).Take(pagination.pageSize).ToList();
        }
    }
}

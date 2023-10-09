using AutoMapper;
using HFS_BE.Models;
using HFS_BE.Ultis;

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

        public List<T> Paginate<T>(List<T> sourceList, PaginationDto pagination)
        {
            return sourceList.Skip((pagination.pageNumber - 1) * pagination.pageSize).Take(pagination.pageSize).ToList();
        }
    }
}

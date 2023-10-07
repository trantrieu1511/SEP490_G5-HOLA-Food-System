using HFS_BE.Ultis;

namespace HFS_BE.Base
{
    public class BaseInputDto
    {
        public PaginationDto? Pagination { get; set; }
        public UserDto? UserInfo { get; set; }
        //public List<ValidationErrorDto> ValidationErrors { get; set; }
    }
}

using HFS_BE.Base;

namespace HFS_BE.Controllers.Auth
{
    public class TokenApiModelOutput : BaseOutputDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

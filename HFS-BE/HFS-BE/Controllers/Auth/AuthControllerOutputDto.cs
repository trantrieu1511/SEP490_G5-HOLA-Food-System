using HFS_BE.Base;
using HFS_BE.Services;

namespace HFS_BE.Controllers.Auth
{
    public class TokenApiModelOutput : BaseOutputDto
    {
        public string? Token { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}

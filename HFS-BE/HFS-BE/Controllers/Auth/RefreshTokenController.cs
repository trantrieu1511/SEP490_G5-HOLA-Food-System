using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{
    public class RefreshTokenController : BaseController
    {
        public RefreshTokenController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost]
        [Route("auths/refresh")]
        public TokenApiModelOutput Refresh(TokenApiModel tokenApiModel)
        {
            // check input
            if (tokenApiModel is null)
                return Output<TokenApiModelOutput>(Constants.ResultCdFail, "Invalid client request");
            TokenApiModelBL inputBusi = new TokenApiModelBL
            {
                AccessToken = tokenApiModel.AccessToken,
                RefreshToken = tokenApiModel.RefreshToken,
                UserDto = GetUserInfor()
            };

            var busi = GetBusinessLogic<RefreshTokenBusinessLogic>();
            return busi.Refresh(inputBusi);
        }
    }
}

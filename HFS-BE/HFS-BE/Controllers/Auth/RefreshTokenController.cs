using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Auth
{
    public class RefreshTokenController : BaseControllerAuth
    {
        public RefreshTokenController(SEP490_HFS_2Context context, IMapper mapper, ITokenService tokenService) : base(context, mapper, tokenService)
        {
        }

        [HttpPost]
        [Route("auths/refresh")]
        public async Task<ActionResult<TokenApiModelOutput>> Refresh(TokenApiModel tokenApiModel)
        {
            // check input
            if (tokenApiModel is null)
                return Unauthorized("Invalid client request");
            TokenApiModelBL inputBusi = new TokenApiModelBL
            {
                RefreshToken = tokenApiModel.RefreshToken,
            };

            var busi = GetBusinessLogic<RefreshTokenBusinessLogic>();
            var output = busi.Refresh(inputBusi);
            if(!output.Success)
                return Unauthorized("Invalid client request");

            //return Unauthorized("Invalid client request");
            return Ok(output);
        }
    }
}

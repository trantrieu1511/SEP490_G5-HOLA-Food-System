using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom.Compiler;

namespace HFS_BE.Controllers.Auth
{
    public class RevokeTokenController : BaseController
    {
        public RevokeTokenController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost]
        [Route("auths/revoke")]
        public BaseOutputDto Revoke(TokenApiModel tokenApiModel)
        {
           
            var busi = GetBusinessLogic<RevokeTokenBusinessLogic>();

            return busi.RevokeToken(tokenApiModel);
        }
    }
}

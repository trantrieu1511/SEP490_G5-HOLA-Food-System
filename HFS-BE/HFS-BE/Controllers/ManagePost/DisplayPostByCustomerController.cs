using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManagePost
{
    public class DisplayPostByCustomerController : BaseController
    {
        public DisplayPostByCustomerController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("users/getNewFeed")]

        public ListPostOutputCustomerDto listpost(PostStatusInputDto inputDto)
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayPostByCustomerBusinessLogic>();

                return business.ListPostsByCustomer(inputDto);
              
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputCustomerDto>(Constants.ResultCdFail);
            }
        }
    }
}

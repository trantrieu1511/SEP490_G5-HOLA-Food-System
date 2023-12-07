using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Category;
using HFS_BE.BusinessLogic.CommentNewFeed;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Category
{
    public class GetAllCategoryController : BaseController
    {
        public GetAllCategoryController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("users/getallcategory")]
        public ListcategoryOutptuDto GetAll()
        {
            try
            {

                var business = this.GetBusinessLogic<GetAllCategoryBusinessLogic>();

                var output = business.GetAll();

                return output;
            }
            catch (Exception)
            {
                return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
            }
        }
    }
}

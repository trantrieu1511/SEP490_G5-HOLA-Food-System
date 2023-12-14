using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Search;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Search
{
    public class GetCategorySearchController : BaseController
    {
        public GetCategorySearchController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("home/getcategory")]
        public ListcategoryOutptuDto GetAll()
        {
            try
            {

                var business = this.GetBusinessLogic<GetCategorySearchBusinessLogic>();

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

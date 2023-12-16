using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Category;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Category
{
    public class GetCategoryDisplayController : BaseController
    {
        public GetCategoryDisplayController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("categories/getAllCategoryDisplay")]
        public ListcategoryOutptuDto GetAll()
        {
            try
            {

                var business = this.GetBusinessLogic<GetCategoryDisplayBusinessLogic>();

                var output = business.GetCategoryDisplay();

                return output;
            }
            catch (Exception)
            {
                return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
            }
        }
    }
}

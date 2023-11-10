using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Category;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Category
{

    public class UpdateCategoryController : BaseController
    {
        public UpdateCategoryController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("updateCategory")]
        public BaseOutputDto UpdateCategory(UpdateCategoryDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<UpdateCategoryBusinessLogic>();
                return busi.UpdateCategory(inputDto);
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

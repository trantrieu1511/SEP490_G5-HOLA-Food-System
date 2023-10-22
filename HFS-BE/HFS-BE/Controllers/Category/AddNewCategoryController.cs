using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Category;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Category
{
    [Authorize(Roles = "1")]
    public class AddNewCategoryController : BaseController
    {
        public AddNewCategoryController(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("createcategory")]
        public BaseOutputDto AddNewCategory(CategoryDaoInputDto inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<AddCategoryBusinessLogic>();
                return busi.AddCategory(inputDto);
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

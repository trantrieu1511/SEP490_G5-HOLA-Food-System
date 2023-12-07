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
    
    public class AddNewCategoryController : BaseController
    {
        public AddNewCategoryController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("users/createcategory")]
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

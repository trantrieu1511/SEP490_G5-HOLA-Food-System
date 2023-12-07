using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Category;
using HFS_BE.BusinessLogic.ManageVoucher;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.Category
{
    public class EDCategoryController : BaseController
    {
        public EDCategoryController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("users/edcategory")]
        public BaseOutputDto EnableDisableCate(Enable_Disable_CateInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<EDCategoryBusinessLogic>();

                var output = business.EnableDisableCate(input);

                // call signalR to Post Modelrator

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

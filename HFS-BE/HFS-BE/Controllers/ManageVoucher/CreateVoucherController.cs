using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManageVoucher;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageVoucher
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateVoucherController : BaseController
    {
        public CreateVoucherController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpPost("foods/addNewVoucher")]
        //[Authorize]

        public BaseOutputDto CreateVoucher([FromForm] CreateVoucherDaoInputDto input)
        {
            try
            {

                var business = this.GetBusinessLogic<AddNewVoucherBusinessLogic>();

                var output = business.AddNewVoucher(input);

                return output;
            }
            catch (Exception)
            {
                return this.Output<CreateVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

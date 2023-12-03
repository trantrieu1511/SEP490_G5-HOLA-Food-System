using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.BusinessLogic.ManageSellerLicenseImage;
using HFS_BE.DAO.SellerLicenseImageDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageSellerLicenseImage
{

    public class EditSellerLicenseImageController : BaseController
    {
        public EditSellerLicenseImageController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPut("users/editLicenseImage")]
        [Authorize]
        public BaseOutputDto EditLicenseImage([FromForm] EditLicenseImageInputDto input)
        {
            try
            {
                var business = this.GetBusinessLogic<EditSellerLicenseImageBusinessLogic>();

                input.UserDto = GetUserInfor();

                var output = business.EditLicenseImage(input);

                return output;
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

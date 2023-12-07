using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageSellerLicenseImage;
using HFS_BE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageSellerLicenseImage
{
    public class GetAllSellerLicenseImageController : BaseController
    {
        public GetAllSellerLicenseImageController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("users/getAllLicenseImages")]
        [Authorize]
        public ListSellerLicenseImageOutputDto GetAllSellerLicenseImages()
        {
            try
            {
                var business = GetBusinessLogic<GetAllSellerLicenseImageBusinessLogic>();
                var output = business.GetAllSellerLicenseImages(GetUserInfor().Email, GetUserInfor().UserId);
                return output;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.BusinessLogic.ManageShipper;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageShipper
{
    public class DisplayShipperAvailableController : BaseController
    {
        public DisplayShipperAvailableController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        [HttpGet("users/getShippersAvailable")]
        public ShipperInforList DisplayShipperAvailable()
        {
            try
            {
                var business = this.GetBusinessLogic<DisplayShipperAvailableBusinessLogic>();
                var inputBL = new ShipperBySellerInputDto
                {
                    User = GetUserInfor()
                };
                return business.DisplayShipperAvailable(inputBL);
            }
            catch (Exception)
            {
                return this.Output<ShipperInforList>(Constants.ResultCdFail);
            }
        }
    }
}

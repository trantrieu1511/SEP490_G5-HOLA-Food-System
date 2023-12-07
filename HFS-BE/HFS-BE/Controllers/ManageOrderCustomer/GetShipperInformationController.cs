using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageOrderCustomer;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageOrderCustomer
{
    public class GetShipperInformationController : BaseController
    {
        public GetShipperInformationController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("orders/shipperinfo")]
        public GetShipperInforOutputDto GetShipper(ListInvitationShipperDtoInput inputDto)
        {
            try
            {
                var busi = this.GetBusinessLogic<GetShipperInforBusinessLogic>();
                return busi.GetShipper(inputDto.ShipperId);
            }
            catch (Exception)
            {
                return this.Output<GetShipperInforOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

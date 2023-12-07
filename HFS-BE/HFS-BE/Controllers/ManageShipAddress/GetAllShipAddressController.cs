using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageShipAddress;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HFS_BE.Controllers.ManageShipAddress
{
    public class GetAllShipAddressController : BaseController
    {
        public GetAllShipAddressController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpGet("customer/getallshipaddress")]
        [Authorize]
        public ListShipAddressOutputDto GetAllShipAddress()
        {
            try
            {
                var business = GetBusinessLogic<GetAllShipAddressBusinessLogic>();
                return business.GetAllShipAddress(GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<ListShipAddressOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

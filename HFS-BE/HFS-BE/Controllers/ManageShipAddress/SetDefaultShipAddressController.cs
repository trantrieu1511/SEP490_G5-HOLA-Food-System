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
    public class SetDefaultShipAddressController : BaseController
    {
        public SetDefaultShipAddressController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("customer/setdefaultshipaddress")]
        [Authorize]
        public BaseOutputDto SetDefaultShipAddress(SetDefaultShipAddressInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<SetDefaultShipAddressBusinessLogic>();
                return business.SetDefaultShipAddress(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

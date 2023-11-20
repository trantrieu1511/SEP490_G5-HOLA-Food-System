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
    public class UpdateShipAddressController : BaseController
    {
        public UpdateShipAddressController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPut("customer/updateshipaddress")]
        [Authorize]
        public BaseOutputDto UpdateShipAddress(UpdateShipAddressInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<UpdateShipAddressBusinessLogic>();
                return business.UpdateShipAddress(inputDto);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

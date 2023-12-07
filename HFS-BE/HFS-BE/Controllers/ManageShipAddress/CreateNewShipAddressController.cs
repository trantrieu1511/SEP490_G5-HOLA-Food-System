﻿using AutoMapper;
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
    public class CreateNewShipAddressController : BaseController
    {
        public CreateNewShipAddressController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("customer/createnewshipaddress")]
        [Authorize]
        public BaseOutputDto CreateNewShipAddress(CreateNewShipAddressInputDto inputDto)
        {
            try
            {
                var business = GetBusinessLogic<CreateNewShipAddressBusinessLogic>();
                return business.CreateNewShipAddress(inputDto, GetUserInfor().UserId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

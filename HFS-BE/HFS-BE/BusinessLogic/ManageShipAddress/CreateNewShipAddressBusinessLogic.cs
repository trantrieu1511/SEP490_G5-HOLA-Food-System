using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManageShipAddress
{
    public class CreateNewShipAddressBusinessLogic : BaseBusinessLogic
    {
        public CreateNewShipAddressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto CreateNewShipAddress(CreateNewShipAddressInputDto inputDto, string userId) {
            try
            {
                var dao = CreateDao<ShipAddressDao>();
                return dao.CreateNewShipAddress(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

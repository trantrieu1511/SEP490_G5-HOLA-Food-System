using HFS_BE.Base;

namespace HFS_BE.DAO.ShipAddressDao
{
    public class ShipAddressOutputDto
    {
        public int AddressId { get; set; }
        public string AddressInfo { get; set; } = string.Empty;
        public bool? IsDefaultAddress { get; set; }
    }

    public class ListShipAddressOutputDto : BaseOutputDto
    {
        public List<ShipAddressOutputDto> ShipAddresses { get; set; } = new List<ShipAddressOutputDto>();
    }
}

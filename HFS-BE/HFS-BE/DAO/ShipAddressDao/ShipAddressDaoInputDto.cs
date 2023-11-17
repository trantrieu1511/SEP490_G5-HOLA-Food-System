namespace HFS_BE.DAO.ShipAddressDao
{
    public class CreateNewShipAddressInputDto
    {
        public string AddressInfo { get; set; } = null!;
    }
    
    public class UpdateShipAddressInputDto
    {
        public int AddressId { get; set; }
        public string AddressInfo { get; set; } = null!;
    }
    
    public class DeleteShipAddressInputDto
    {
        public int AddressId { get; set; }
    }
    
    public class SetDefaultShipAddressInputDto
    {
        public int AddressId { get; set; }
    }
}

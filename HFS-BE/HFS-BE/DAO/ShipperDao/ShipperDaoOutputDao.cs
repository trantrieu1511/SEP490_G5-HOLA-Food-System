using HFS_BE.Base;

namespace HFS_BE.DAO.ShipperDao
{
    public class ShipperInfor
    {
        public string? ShipperId { get; set; }
        public string? ShipperName { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public long? PhoneNumber { get; set; }
        public string? Avatar { get; set; }
    }

    public class ShipperInforList: BaseOutputDto
    {
        public List<ShipperInfor>? Shippers { get; set; }
    }
}

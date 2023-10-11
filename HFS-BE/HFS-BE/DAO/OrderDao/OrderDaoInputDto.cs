using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Dao.OrderDao
{
    public class OrderByShipperDaoInputDto
    {
        [Required(ErrorMessage = "Shipper required!")]
        public int? ShipperId { get; set; }
    }
}

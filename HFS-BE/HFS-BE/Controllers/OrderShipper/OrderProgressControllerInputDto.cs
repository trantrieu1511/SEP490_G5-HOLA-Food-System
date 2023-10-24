using HFS_BE.Utils;

namespace HFS_BE.Controllers.OrderShipper
{
    public class OrderProgressControllerInputDto
    {
        public int OrderId { get; set; }
        public string? Note { get; set; }
        public byte Status { get; set; }
        public IFormFile? Image { get; set; }

        
    }
}

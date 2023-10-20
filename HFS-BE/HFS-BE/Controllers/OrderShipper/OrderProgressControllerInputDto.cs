using HFS_BE.Utils;

namespace HFS_BE.Controllers.OrderShipper
{
    public class OrderProgressControllerInputDto
    {
        public int OrderId { get; set; }
        public string Note { get; set; }
        public bool Type { get; set; }
        public IFormFile Image { get; set; }

        
    }
}

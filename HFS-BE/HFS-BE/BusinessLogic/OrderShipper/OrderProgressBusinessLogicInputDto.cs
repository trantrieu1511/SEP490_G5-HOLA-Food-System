using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderProgressBusinessLogicInputDto
    {
        public int OrderId { get; set; }
        public string? Check { get; set; }
        public string? Note { get; set; }
        public byte Status { get; set; }
        public IFormFile? Image { get; set; }

        public UserDto UserDto { get; set; }
    }
}

using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Food
{
    public class FoodCreateInputDto
    {
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public IReadOnlyList<IFormFile> Images { get; set; }

        public UserDto UserDto { get; set; }
    }
}

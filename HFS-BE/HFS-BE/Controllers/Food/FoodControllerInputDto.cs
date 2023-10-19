namespace HFS_BE.Controllers.Food
{
    public class FoodCreateInputDto
    {
        public string? Name { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public IReadOnlyList<IFormFile> Images { get; set; }

    }
}

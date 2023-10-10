using HFS_BE.Base;

namespace HFS_BE.Dao.FoodDao
{
    public class FoodOutputDto
    {
        public int FoodId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }
    }

    public class DisplayFoodOutputDto : BaseOutputDto
    {
        public List<FoodOutputDto> ListFood { get; set; }
    }
}

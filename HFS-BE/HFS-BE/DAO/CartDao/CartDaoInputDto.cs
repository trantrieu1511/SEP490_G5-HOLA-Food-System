using System.ComponentModel.DataAnnotations;

namespace HFS_BE.DAO.CartDao
{
    public class GetCartItemDaoInputDto
    {
        public string CartId { get; set; }
    }

    public class AddCartItemInputDto
    {
        public string? CartId { get; set; }
        [Required(ErrorMessage = "FoodId Required")]
        public int? FoodId { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must > 0")]
        public int? Amount { get; set; }
    }

    public class DeleteCartItemInputDto
    {
        public string? CartId { get; set; }
        [Required(ErrorMessage = "FoodId Required")]
        public int? FoodId { get; set; }
        public int? Amount { get; set; }
    }

    public class UpdateAmoutCartItemDaoInputDto
    {
        public string? CartId { get; set; }
        [Required(ErrorMessage = "FoodId Required")]
        public int? FoodId { get; set; }
        [Required(ErrorMessage = "Amount Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Amount must > 0")]
        public int? Amount { get; set; }
    }  
}

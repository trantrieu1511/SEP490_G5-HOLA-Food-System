namespace HFS_BE.DAO.CartDao
{
    public class GetCartItemDaoInputDto
    {
        public int CartId { get; set; }
    }

    public class AddCartItemInputDto
    {
        public int CartId { get; set; }
        public int FoodId { get; set; }
        public int Amount { get; set; }
    }
}

using HFS_BE.Base;


namespace HFS_BE.Dao.OrderDao
{
    public class OrderHistoryOutputDto
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string? ShipAddress { get; set; }
        public int? ShipperId { get; set; }
        public int? VoucherId { get; set; }
        public bool? Status { get; set; }

        public List<OrderDetailDto> OrderDetails { get; set; }
    }

    public class OrderOnHistoryDaoOutputDto : BaseOutputDto
    {
        public List<OrderHistoryOutputDto> OrderList { get; set; }
    }   
}

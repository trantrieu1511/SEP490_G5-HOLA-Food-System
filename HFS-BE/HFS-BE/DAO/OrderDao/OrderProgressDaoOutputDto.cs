namespace HFS_BE.DAO.OrderDao
{
    public class OrderProgressDaoOutputDto
    {
        public int OrderProgressId { get; set; }
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Image { get; set; }
        public int? OrderId { get; set; }
        public bool? Status { get; set; }
        public int? UserId { get; set; }

    }
}

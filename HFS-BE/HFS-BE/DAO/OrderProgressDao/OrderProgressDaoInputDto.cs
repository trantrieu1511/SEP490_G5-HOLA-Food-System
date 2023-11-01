using HFS_BE.Base;

namespace HFS_BE.DAO.OrderProgressDao
{
    public class OrderProgressDaoInputDto 
    {
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Image { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public int? UserId { get; set; }
    }

    public class OrderCreateDaoInputDto
    {
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public string? UserId { get; set; }
    }

    public class OrderProgressStatusInputDto
    {
        public int OrderId { get; set; }
        public byte Status { get; set; }
        public string? UserId { get; set; }
    }

    public class OrderProgressCancelInputDto
    {
        public int OrderId { get; set; }
        public byte Status { get; set; }
        public string? UserId { get; set; }
        public string? Note { get; set; }
    }
}

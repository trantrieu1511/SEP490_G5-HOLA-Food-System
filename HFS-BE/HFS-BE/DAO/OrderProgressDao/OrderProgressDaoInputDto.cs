using HFS_BE.Base;

namespace HFS_BE.DAO.OrderProgressDao
{
    public class OrderProgressDaoInputDto : BaseInputDto
    {
        public string? Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Image { get; set; }
        public int? OrderId { get; set; }
        public byte? Status { get; set; }
        public int? UserId { get; set; }
    }
}

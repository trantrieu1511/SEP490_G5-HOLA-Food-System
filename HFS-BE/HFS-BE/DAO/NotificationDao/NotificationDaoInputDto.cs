namespace HFS_BE.DAO.NotificationDao
{
    public class NotificationAddNewInputDto
    {
        public string? SendBy { get; set; }
        public string? Receiver { get; set; }
        public int TypeId { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

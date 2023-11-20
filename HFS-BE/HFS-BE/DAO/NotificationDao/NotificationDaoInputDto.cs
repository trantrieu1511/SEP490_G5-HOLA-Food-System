using System.ComponentModel.DataAnnotations;

namespace HFS_BE.DAO.NotificationDao
{
    public class NotificationAddNewInputDto
    {
        public int Lang { get; set; }
        public string? SendBy { get; set; }
        public string? Receiver { get; set; }
        public int Type { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class NotificationInput
    {
        public string? Receiver { get; set; }
    }

    public class NotificationGetInput
    {
        public string? Receiver { get; set; }
        public int TakeNum { get; set; }
        public string Lang { get; set; }
    }

    public class NotificationReadInput
    {
        [Required(ErrorMessage = "NotifyId not empty")]
        public int NotifyId { get; set; }
        public string Lang { get; set; }
    }
}

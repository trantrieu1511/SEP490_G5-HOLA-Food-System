﻿using HFS_BE.Base;

namespace HFS_BE.DAO.NotificationDao
{
    public class NotificationDaoOutputDto
    {
        public int Id { get; set; }
        public string? SendBy { get; set; }
        public string? Receiver { get; set; }
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? CreateDate { get; set; }
        public bool? IsRead { get; set; }
    }

    public class NotificationLst : BaseOutputDto
    {
        public List<NotificationDaoOutputDto>? Notifies { get; set; }
    }
}
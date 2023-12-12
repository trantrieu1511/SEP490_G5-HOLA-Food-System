﻿using HFS_BE.Utils;

namespace HFS_BE.Dao.PostDao
{
    public class PostCreateInputDto
    {
        public string? PostContent { get; set; }
        public List<string> Images { get; set; }

        public UserDto UserDto { get; set; }

    }
    public class PostStatusInputDto
    {
        public string? CustomerId { get; set; }
        public byte status { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
    public class PostEnableDisableInputDto
    {
        public int PostId { get; set; }
        public bool Type { get; set; }
        public UserDto? UserDto { get; set; }
    }

    public class PostBanUnbanInputDto
    {
        public int PostId { get; set; }
        public bool isBanned { get; set; }
        public string BanNote { get; set; } = string.Empty;
    }

    public class PostUpdateInputDto
    {
        public int PostId { get; set; }
        public string? PostContent { get; set; }

    }
}

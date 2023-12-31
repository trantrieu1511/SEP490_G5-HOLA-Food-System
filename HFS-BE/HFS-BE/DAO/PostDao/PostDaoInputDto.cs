﻿using HFS_BE.Utils;

namespace HFS_BE.Dao.PostDao
{
    public class PostCreateInputDto
    {
        public string? PostContent { get; set; }
        public List<string> Images { get; set; }

        public UserDto UserDto { get; set; }
    }

    public class PostEnableDisableInputDto
    {
        public int PostId { get; set; }
        public bool Type { get; set; }
    }

    public class PostUpdateInputDto
    {
        public int PostId { get; set; }
        public string? PostContent { get; set; }

    }
}

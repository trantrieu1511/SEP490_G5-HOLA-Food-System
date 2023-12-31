﻿using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.ManagePost
{
    /*public class PostOutputDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

    }

    public class ListPostOutputDto : BaseOutputDto
    {
        public List<PostOutputDto> Posts { get; set; }
    }*/


    public class PostOutputSellerDto
    {
        public int PostId { get; set; }
        public int? ShopId { get; set; }
        public string? PostContent { get; set; }

        public string? CreatedDate { get; set; }

        public string? Status { get; set; }

        public List<PostImageOutputSellerDto>? ImagesBase64 { get; set; } = new List<PostImageOutputSellerDto>();
    }

    public class PostImageOutputSellerDto
    {
        public int ImageId { get; set; }
        public string? ImageBase64 { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }
    }

    public class ListPostOutputSellerDto : BaseOutputDto
    {
        public List<PostOutputSellerDto> Posts { get; set; }
    }

}

using HFS_BE.Base;
using HFS_BE.Models;

namespace HFS_BE.Dao.PostDao
{
    public class PostOutputDto 
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }

        public List<PostImage> PostImages { get; set; }
    }

    public class ListPostOutputDto : BaseOutputDto
    {
        public List<PostOutputDto> Posts { get; set; }
    }


    public class PostOutputSellerDto
    {
        public int PostId { get; set; }
        public string? SellerId { get; set; }
        public string? PostContent { get; set; }

        public string? CreatedDate { get; set; }

        public string? Status { get; set; }

        public List<PostImage>? Images { get; set; }
    }

    public class ListPostOutputSellerDto : BaseOutputDto
    {
        public List<PostOutputSellerDto> Posts { get; set; }
    }

    public class PostByCustomerOutputDto
    {
        public int PostId { get; set; }
        public string? SellerId { get; set; }
        public string? ShopName { get; set; }
        public string? PostContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte? Status { get; set; }
        public List<PostImage> PostImages { get; set; }

    }

    public class ListPostByCustomerOutputDto : BaseOutputDto
    {
        public List<PostByCustomerOutputDto> Posts { get; set; }
    }
}

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

    public class DashboardPostmodPostDataDaoOutputDto : BaseOutputDto
    {
        public List<Post> Posts { get; set; } = new List<Post>();
    }


    public class PostOutputSellerDto
    {
        public int PostId { get; set; }
        public string? SellerId { get; set; }
        public string? SellerEmail { get; set; }
        public string? PostContent { get; set; }

        public string? CreatedDate { get; set; }

        //public int? ReportedTimes { get; set; }

        public string? BanBy { get; set; }

        public DateTime? BanDate { get; set; }

        public string? BanNote { get; set; }

        public string? Status { get; set; }

        public List<PostImage>? Images { get; set; }
    }

    public class ListPostOutputSellerDto : BaseOutputDto
    {
        public List<PostOutputSellerDto> Posts { get; set; }
    }

    public class PostByCustomerOutputDto
    {
        public bool isLiked { get; set; } = false;
        public int LikeCount { get; set; }
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
        public int TotalPosts { get; set; }
        public int TotalPages { get; set; }
         public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class AddNewPostOutput : BaseOutputDto
    {
        public int PostId { get; set; }
    }
}

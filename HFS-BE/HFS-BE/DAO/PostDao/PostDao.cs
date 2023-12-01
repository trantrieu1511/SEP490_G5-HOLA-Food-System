using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.Dashboard;
using HFS_BE.Dao.FoodDao;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HFS_BE.Dao.PostDao
{
    public class PostDao : BaseDao
    {
        public PostDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        //public ListPostOutputDto ListAllPosts()
        //{
        //    try
        //    {
        //        /*var data = (from post in context.Posts
        //                    join user in context.Users
        //                    on post.UserId equals user.UserId
        //                    join postImg in context.PostImages
        //                    on post.PostId equals postImg.PostId
        //                    select new PostOutputDto
        //                    {
        //                        PostId = post.PostId,
        //                        UserId = post.UserId,
        //                        UserFirstName = user.FirstName,
        //                        PostContent = post.PostContent,
        //                        Status = post.Status,
        //                        CreatedDate = post.CreatedDate,
        //                        postImages = context.PostImages.Where(pi => pi.PostId == post.PostId).ToList()
        //                    }).ToList();*/
        //        List<PostOutputDto> data = context.Posts
        //            .Include(p => p.Seller)
        //            .Include(p => p.PostImages)
        //            .Select(p => new PostOutputDto
        //            {
        //                PostId = p.PostId,
        //                //UserId = p.SellerId,
        //                UserFirstName = p.Seller.FirstName,
        //                PostContent = p.PostContent,
        //                CreatedDate = p.CreatedDate,
        //                //Status = p.Status,
        //                PostImages = p.PostImages.ToList()
        //            }).ToList();

        //        var output = this.Output<ListPostOutputDto>(Constants.ResultCdSuccess);
        //        //output.Posts = mapper.Map<List<Post>, List<PostOutputDto>>(data);
        //        output.Posts = data;
        //        return output;
        //    }
        //    catch (Exception e)
        //    {

        //        return this.Output<ListPostOutputDto>(Constants.ResultCdFail);
        //    }
        //}

        public DashboardPostmodPostDataDaoOutputDto GetAllPosts()
        {
            try
            {
                List<Post> data = context.Posts.ToList();

                var output = this.Output<DashboardPostmodPostDataDaoOutputDto>(Constants.ResultCdSuccess);
                //output.Posts = mapper.Map<List<Post>, List<PostOutputDto>>(data);
                output.Posts = data;
                return output;
            }
            catch (Exception e)
            {

                return this.Output<DashboardPostmodPostDataDaoOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public ListPostOutputSellerDto GetAllPostSeller(UserDto userDto)
        {
            try
            {
                List<PostOutputSellerDto> postsModel = context.Posts
                                        .Include(p => p.PostImages)
                                        .Where(p => p.SellerId == userDto.UserId)
                                        .OrderBy(p => p.CreatedDate)
                                        .Select(p => new PostOutputSellerDto
                                        {
                                            PostId = p.PostId,
                                            CreatedDate = p.CreatedDate.Value.ToString("MM/dd/yyyy"),
                                            PostContent = p.PostContent,
                                            Status = PostMenuStatusEnum.GetStatusString(p.Status),
                                            Images = p.PostImages.ToList()
                                        })
                                        .ToList();
                var output = this.Output<ListPostOutputSellerDto>(Constants.ResultCdSuccess);
                output.Posts = postsModel;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }

        public ListPostOutputSellerDto GetAllPostPostModerator()
        {
            try
            {
                List<PostOutputSellerDto> posts = context.Posts
                                        .Include(p => p.PostImages)
                                        .Select(p => new PostOutputSellerDto
                                        {
                                            PostId = p.PostId,
                                            SellerId = p.SellerId,
                                            CreatedDate = p.CreatedDate.Value.ToString("MM/dd/yyyy"),
                                            PostContent = p.PostContent,
                                            Status = PostMenuStatusEnum.GetStatusString(p.Status),
                                            Images = p.PostImages.ToList(),
                                            //ReportedTimes = p.ReportedTimes,
                                            BanBy = p.BanBy,
                                            BanDate = p.BanDate,
                                            BanNote = p.BanNote
                                        })
                                        .ToList();
                var output = this.Output<ListPostOutputSellerDto>(Constants.ResultCdSuccess);
                output.Posts = posts;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail, e.Message + "\n" + e.Source + "\n" + e.StackTrace + "\n" + e.InnerException);
            }
        }

        public AddNewPostOutput AddNewPost(PostCreateInputDto postDto)
        {
            try
            {

                // Add post
                Post post = new Post
                {
                    CreatedDate = DateTime.Now,
                    PostContent = postDto.PostContent,
                    Status = 1,
                    SellerId = postDto.UserDto.UserId
                };
                context.Add(post);
                context.SaveChanges();

                if (postDto.Images != null)
                {
                    foreach (var img in postDto.Images)
                    {
                        context.Add(new PostImage
                        {
                            PostId = post.PostId,
                            Path = img
                        });
                        context.SaveChanges();
                    }
                }

                var output = this.Output<AddNewPostOutput>(Constants.ResultCdSuccess);
                output.PostId = post.PostId;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<AddNewPostOutput>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto EnableDisablePost(PostEnableDisableInputDto input)
        {
            try
            {
                // Get post
                var post = context.Posts.FirstOrDefault(x => x.PostId == input.PostId);

                if (input.Type)
                {
                    // set status Display
                    post.Status = 1;
                    context.SaveChanges();
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                //set status Hide
                post.Status = 2;
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto BanUnbanPost(PostBanUnbanInputDto input, string userId)
        {
            try
            {
                // Check user role
                if (userId.Substring(0, 2) != "PM")
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Please login as a post moderator before using this API");
                }
                // Check ban limit (25 per day), neu lon hon 0 thi moi thuc hien viec ban, neu khong thi se khong thuc hien viec ban nua. 
                // Phong truong hop co nguoi lam nguoi khong lam (BanLimit duoc reset vao 23h59 moi ngay)
                if (context.PostModerators.SingleOrDefault(pm => pm.ModId.Equals(userId)).BanLimit > 0)
                {
                    // Get post
                    var post = context.Posts.SingleOrDefault(p => p.PostId == input.PostId);

                    // Check if found or not
                    if (post == null)
                    {
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "Post not found");
                    }

                    if (input.isBanned)
                    {
                        // set status banned
                        post.Status = 3;
                        post.BanBy = userId;
                        post.BanDate = DateTime.Now;
                        post.BanNote = input.BanNote;
                    }
                    else
                    {
                        //set status display
                        post.Status = 1;
                        post.BanBy = null;
                        post.BanDate = null;
                        post.BanNote = null;
                    }

                    // Reduce ban/unban limit
                    context.PostModerators.SingleOrDefault(pm => pm.ModId.Equals(userId)).BanLimit -= 1;

                    context.SaveChanges();
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                else
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Your limit of banning 25 posts per day have reached.");
                }
            }
            catch (Exception e)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        public BaseOutputDto UpdatePost(PostUpdateInputDto input)
        {
            try
            {
                var postModel = context.Posts.FirstOrDefault(
                        f => f.PostId == input.PostId
                    );
                if (postModel == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"PostId: {input.PostId} not exist!");

                postModel.PostContent = input.PostContent;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                //log error
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public Post? GetPostById(int postId)
        {
            return context.Posts.Include(x => x.Seller).FirstOrDefault(x => x.PostId == postId);
        }

        public ListPostByCustomerOutputDto ListPostsByCustomer(PostStatusInputDto input)
        {
            try
            {

                List<PostByCustomerOutputDto> data = context.Posts
                    .Include(p => p.Seller)
                    .Include(p => p.PostImages)
                    .Select(p => new PostByCustomerOutputDto
                    {
                        PostId = p.PostId,
                        SellerId = p.SellerId,
                        ShopName = p.Seller.ShopName,
                        PostContent = p.PostContent,
                        CreatedDate = p.CreatedDate,
                        PostImages = p.PostImages.ToList(),
                        Status = p.Status
                    }).Where(s => s.Status == input.status).ToList();

                var output = this.Output<ListPostByCustomerOutputDto>(Constants.ResultCdSuccess);
                output.Posts = data;
                return output;
            }
            catch (Exception e)
            {

                return this.Output<ListPostByCustomerOutputDto>(Constants.ResultCdFail);
            }
        }

        public DashboardPostModStatisticOutput GetAllTimeStatistics(string userId)
        {
            try
            {
                var allTimeStatistics = new DashboardPostModStatistic
                {
                    TotalPosts = context.Posts.Count(),
                    TotalBannedPosts = context.Posts.Where(p => p.Status == 3).Count(),
                    TotalPostReports = context.PostReports.Count(),
                    TotalPendingPostReports = context.PostReports.Where(pr => pr.Status == 0).Count(),
                    TotalApprovedPostReports = context.PostReports.Where(pr => pr.Status == 1).Count(),
                    TotalNotapprovedPostReports = context.PostReports.Where(pr => pr.Status == 2).Count()
                };
                var myAllTimeStatistics = new DashboardPostModStatistic
                {
                    TotalApprovedPostReports = context.PostReports.Where(pr => pr.Status == 1 && pr.UpdateBy.Equals(userId)).Count(),
                    TotalNotapprovedPostReports = context.PostReports.Where(pr => pr.Status == 2 && pr.UpdateBy.Equals(userId)).Count()
                };
                var output = Output<DashboardPostModStatisticOutput>(Constants.ResultCdSuccess);
                output.Statistics = allTimeStatistics;
                output.MyStatistics = myAllTimeStatistics;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
            }
        }

        //public DashboardPostModStatisticOutput GetThisMonthStatistics(string userId)
        //{
        //    try
        //    {
        //        var thisMonthStatistics = new DashboardPostModStatistic
        //        {
        //            TotalPosts = context.Posts.Where(p => Convert.ToDateTime(p.CreatedDate).Month == DateTime.Now.Month).Count(),
        //            TotalBannedPosts = context.Posts.Where(p => Convert.ToDateTime(p.CreatedDate).Month == DateTime.Now.Month && p.Status == 3).Count(),
        //            TotalPendingPostReports = context.PostReports.Where(pr => Convert.ToDateTime(pr.CreateDate).Month == DateTime.Now.Month && pr.Status == 0).Count(),
        //            TotalApprovedPostReports = context.PostReports.Where(pr => Convert.ToDateTime(pr.CreateDate).Month == DateTime.Now.Month && pr.Status == 1).Count(),
        //            TotalNotapprovedPostReports = context.PostReports.Where(pr => Convert.ToDateTime(pr.CreateDate).Month == DateTime.Now.Month && pr.Status == 2).Count()
        //        };
        //        var output = Output<DashboardPostModStatisticOutput>(Constants.ResultCdSuccess);
        //        output.Statistics = thisMonthStatistics;
        //        return output;
        //    }
        //    catch (Exception e)
        //    {
        //        return this.Output<DashboardPostModStatisticOutput>(Constants.ResultCdFail, e.Message + e.Source + e.InnerException + e.StackTrace);
        //    }
        //}
    }
}

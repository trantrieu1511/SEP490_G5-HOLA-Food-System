using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.FoodDao;
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

        public ListPostOutputDto ListAllPosts()
        {
            try
            {
                /*var data = (from post in context.Posts
                            join user in context.Users
                            on post.UserId equals user.UserId
                            join postImg in context.PostImages
                            on post.PostId equals postImg.PostId
                            select new PostOutputDto
                            {
                                PostId = post.PostId,
                                UserId = post.UserId,
                                UserFirstName = user.FirstName,
                                PostContent = post.PostContent,
                                Status = post.Status,
                                CreatedDate = post.CreatedDate,
                                postImages = context.PostImages.Where(pi => pi.PostId == post.PostId).ToList()
                            }).ToList();*/
                List<PostOutputDto> data = context.Posts
                    .Include(p => p.Seller)
                    .Include(p => p.PostImages)
                    .Select(p => new PostOutputDto
                    {
                        PostId = p.PostId,
                        //UserId = p.SellerId,
                        UserFirstName = p.Seller.FirstName,
                        PostContent = p.PostContent,
                        CreatedDate = p.CreatedDate,
                        //Status = p.Status,
                        PostImages = p.PostImages.ToList()
                    }).ToList();

                var output = this.Output<ListPostOutputDto>(Constants.ResultCdSuccess);
                //output.Posts = mapper.Map<List<Post>, List<PostOutputDto>>(data);
                output.Posts = data;
                return output;
            }
            catch (Exception e)
            {

                return this.Output<ListPostOutputDto>(Constants.ResultCdFail);
            }
        }

        public ListPostOutputSellerDto GetAllPostSeller(UserDto userDto)
        {
            try
            {
                List<PostOutputSellerDto> postsModel = context.Posts
                                        .Include(p => p.PostImages)
                                        .Where(p => p.SellerId == userDto.UserId)
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
                                            Images = p.PostImages.ToList()
                                        })
                                        .ToList();
                var output = this.Output<ListPostOutputSellerDto>(Constants.ResultCdSuccess);
                output.Posts = posts;
                return output;
            }
            catch (Exception e)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto AddNewPost(PostCreateInputDto postDto)
        {
            try
            {

                // Add post
                Post post = new Post
                {
                    CreatedDate = DateTime.Now,
                    PostContent = postDto.PostContent,
                    Status = 0,
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
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception e)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
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

        public BaseOutputDto BanUnbanPost(PostBanUnbanInputDto input)
        {
            try
            {
                // Get post
                var post = context.Posts.SingleOrDefault(x => x.PostId == input.PostId);

                if (input.isBanned)
                {
                    // set status banned
                    post.Status = 3;
                    context.SaveChanges();
                    return Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                //set status display
                post.Status = 1;
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
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
            return context.Posts.FirstOrDefault(x => x.PostId == postId);
        }
    }
}

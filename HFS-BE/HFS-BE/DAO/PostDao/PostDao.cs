using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Dao.PostDao
{
    public class PostDao : BaseDao
    {
        public PostDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListPostOutputDto AllPosts()
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
                    .Include(p => p.User)
                    .Include(p => p.PostImages)
                    .Select(p => new PostOutputDto
                    {
                        PostId = p.PostId,
                        UserId = p.UserId,
                        UserFirstName = p.User.FirstName,
                        PostContent = p.PostContent,
                        CreatedDate = p.CreatedDate,
                        Status = p.Status,
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
    }
}

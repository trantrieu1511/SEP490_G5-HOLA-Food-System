using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Ultis;
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
                var data = (from post in context.Posts
                            join user in context.Users
                            on post.UserId equals user.UserId
                            select new PostOutputDto
                            {
                                PostId = post.PostId,
                                UserId = post.UserId,
                                UserFirstName = user.FirstName,
                                PostContent = post.PostContent,
                                Status = post.Status,
                                CreatedDate = post.CreatedDate,
                                postImages = context.PostImages.Where(pi => pi.PostId == post.PostId).ToList()
                            }).ToList();
                var output = this.Output<ListPostOutputDto>(Constants.ResultCdSuccess);
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

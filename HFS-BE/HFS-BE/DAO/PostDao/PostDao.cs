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

        public ListPostOutputDto AllPosts() {
            try
            {
                List<Post> data = context.Posts
                    .ToList();
                var output = this.Output<ListPostOutputDto>(Constants.ResultCdSuccess);
                output.Posts = mapper.Map<List<Post>, List<PostOutputDto>>(data);
                return output;
                
            }
            catch (Exception e)
            {

                return this.Output<ListPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.CommentNewFeedDao
{
    public class CommentNewFeedDao : BaseDao
    {
        public CommentNewFeedDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateComment(CreateCommentInputDto inputDto)
        {
            try
            {
                var comment = new Comment()
                {
                    PostId = inputDto.PostId,
                    CustomerId= inputDto.CustomerId,
                    CommentContent= inputDto.CommentContent,
                    CreatedDate= DateTime.Now,
                };

                this.context.Add(comment);
                this.context.SaveChanges();
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetCommentByPostOutputDto GetCommentByPost(GetCommentInputDto inputDto)
        {
            try
            {
                var data = context.Comments
                    .Include(x => x.Customer)
                    .Where(x => x.PostId == inputDto.PostId)
                    .ToList();
                var output = this.Output<GetCommentByPostOutputDto>(Constants.ResultCdSuccess);
                output.ListComment = mapper.Map<List<Comment>, List<CommentOutputDto>>(data);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetCommentByPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

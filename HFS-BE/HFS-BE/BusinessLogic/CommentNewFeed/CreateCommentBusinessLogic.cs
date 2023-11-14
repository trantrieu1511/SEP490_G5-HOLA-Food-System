using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.DAO.FeedBackVoteDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.CommentNewFeed
{
    public class CreateCommentBusinessLogic : BaseBusinessLogic
    {
        public CreateCommentBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateComment(CreateCommentInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CommentNewFeedDao>();
                var output = dao.CreateComment(inputDto);

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.FeedBackReplyDao
{
    public class FeedBackReplyDao : BaseDao
    {
        public FeedBackReplyDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetReplyByFeedBackIdDaoOutputDto GetReplyByFeedBackId (GetReplyByFeedBackIdDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.FeedbackReplies
                    .Include(x => x.User)
                    .Include(x => x.Seller)
                    .Where(x => x.FeedbackId == inputDto.FeedBackId)
                    .ToList();

                var output = this.Output<GetReplyByFeedBackIdDaoOutputDto>(Constants.ResultCdSuccess);
                output.ReplyList = mapper.Map<List<FeedbackReply>, List<FeedBackReplyDaoOutputDto>>(data);
                return output;
            }
            catch (Exception)
            {
                return this.Output<GetReplyByFeedBackIdDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

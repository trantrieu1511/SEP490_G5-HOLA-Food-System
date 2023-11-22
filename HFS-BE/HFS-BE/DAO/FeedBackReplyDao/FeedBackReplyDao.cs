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
                    .Include(x => x.Customer)
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
		public BaseOutputDto CreateReplyByFeedBackBySeller(CreateFeedBackbySellerDaoInputDto inputDto)
		{
			try
			{

				FeedbackReply reply = new FeedbackReply();
				reply.SellerId = inputDto.SellerId;
				reply.ReplyMessage = inputDto.ReplyMessage;
				reply.CreatedDate = DateTime.Now;
				reply.FeedbackId = inputDto.FeedbackId;
				reply.CustomerId = inputDto.CustomerId;
				context.FeedbackReplies.Add(reply);
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
		public ListFeedBackbySellerDaoOutputDto GetFeedBackBySeller(string sellerId)
		{
			try
			{
				var data = this.context.Feedbacks
					.Include(x => x.Customer)
                    .Include(x=>x.Food).ThenInclude(s=>s.Seller)
					.Where(x => x.Food.Seller.SellerId== sellerId)
					.ToList();

				var output = this.Output<ListFeedBackbySellerDaoOutputDto>(Constants.ResultCdSuccess);
				output.FeedBacks = mapper.Map<List<Feedback>, List<FeedBackBySellerDaoOutputDto>>(data);
				foreach (var e in output.FeedBacks)
				{
					e.Images = context.FeedBackImages.Where(s => s.FeedbackId == e.FeedbackId).ToList();
				}
				return output;
			}
			catch (Exception)
			{
				return this.Output<ListFeedBackbySellerDaoOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}

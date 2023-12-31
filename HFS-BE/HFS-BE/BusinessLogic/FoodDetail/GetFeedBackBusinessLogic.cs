﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.DAO.FeedBackReplyDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.FoodDetail
{
    public class GetFeedBackBusinessLogic : BaseBusinessLogic
    {
        public GetFeedBackBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetFeedBackOutputDto GetFeedBack(GetFeedBackInputDto inputDto)
        {
            try
            {
                var feedbackDao = this.CreateDao<FeedBackDao>();
                var replyDao = this.CreateDao<FeedBackReplyDao>();

                var feedbackInput = new GetFeedBackByFoodIdDaoInputDto()
                {
                    FoodId = inputDto.FoodId,
                };

                GetFeedBackByFoodIdDaoOutputDto feedbackOutput = feedbackDao.GetFeedBackByFoodId(feedbackInput);
                foreach(var item in feedbackOutput.FeedBacks)
                {
                    if (inputDto.CustomerId != null && item.ListVoted.Where(x => x.VoteBy.Equals(inputDto.CustomerId)).ToList().Count > 0)
                    {
                        item.IsLiked = true;
                    }
                }

                var output = mapper.Map<GetFeedBackByFoodIdDaoOutputDto, GetFeedBackOutputDto>(feedbackOutput);
                if (inputDto.CustomerId != null && output.FeedBacks.FirstOrDefault(x => x.CustomerId.Equals(inputDto.CustomerId)) != null)
                {
                    output.FeedBacks.FirstOrDefault(x => x.CustomerId.Equals(inputDto.CustomerId)).CanReply = true;
                }

                foreach (var item in output.FeedBacks)
                {
                    var replyInput = new GetReplyByFeedBackIdDaoInputDto
                    {
                        FeedBackId = item.FeedbackId,
                    };

                    GetReplyByFeedBackIdDaoOutputDto replyOutput = replyDao.GetReplyByFeedBackId(replyInput);
                    item.ListReply = mapper.Map<List<FeedBackReplyDaoOutputDto>, List<FeedBackReplyOutputDto>>(replyOutput.ReplyList);
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetFeedBackOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

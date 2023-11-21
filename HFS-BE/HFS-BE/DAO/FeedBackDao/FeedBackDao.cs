using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.FeedBackDao
{
    public class FeedBackDao : BaseDao
    {
        public FeedBackDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public GetFeedBackByFoodIdDaoOutputDto GetFeedBackByFoodId(GetFeedBackByFoodIdDaoInputDto inputDto)
        {
            try
            {
                var data = this.context.Feedbacks
                    .Include(x => x.Customer)
                    .Include(x => x.FeedbackVotes)
                    .Where(x => x.FoodId == inputDto.FoodId)
                    .ToList();
                var output = this.Output<GetFeedBackByFoodIdDaoOutputDto>(Constants.ResultCdSuccess);
                output.FeedBacks = mapper.Map<List<Feedback>, List<FeedBackDaoOutputDto>>(data);

                return output;

            }
            catch (Exception)
            {
                return this.Output<GetFeedBackByFoodIdDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto CreateFeedBack(CreateFeedBackDaoInputDto inputDto)
        {
            try
            {
                var feedBack = new Feedback()
                {
                    CustomerId = inputDto.CustomerId,
                    FoodId = inputDto.FoodId,
                    FeedbackMessage = inputDto.FeedbackMessage,
                    Star = inputDto.Star,
                    CreatedDate = DateTime.Now,
                };

                this.context.Add(feedBack);
                this.context.SaveChanges();

				foreach (var img in inputDto.Images)
				{
					context.Add(new FeedBackImage
					{
						FeedbackId = feedBack.FeedbackId,
						Path = img
					});
					context.SaveChanges();
				}
				return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.Enum;
using HFS_BE.Utils.IOFile;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

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
                    .Include(x => x.FeedBackImages)
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
		public GetFeedBackByFoodIdImageDaoOutputDto GetFeedBackByFoodIdImage(GetFeedBackByFoodIdDaoInputDto inputDto)
		{
			try
			{
				//			public string? FeedbackMessage { get; set; }
				//public byte? Star { get; set; }
				//public DateTime? DisplayDate { get; set; }
				//public int LikeCount { get; set; }
				//public int DisLikeCount { get; set; }
				//public bool? IsLiked { get; set; }
				//public List<CustomerVoted> ListVoted { get; set; }
				var data = this.context.Feedbacks
							.Include(x => x.Customer)
							.Include(x => x.FeedbackVotes)
							.Include(x => x.FeedBackImages)
							.Where(x => x.FoodId == inputDto.FoodId)
					.ToList();
				var output = this.Output<GetFeedBackByFoodIdImageDaoOutputDto>(Constants.ResultCdSuccess);
				output.FeedBacks = mapper.Map<List<Feedback>, List<FeedBackDaoOutputDtoImage>>(data);
				
                foreach (var e in output.FeedBacks)
                {
                    e.Images = context.FeedBackImages.Where(s => s.FeedbackId == e.FeedbackId).ToList();
				}
				
				return output;

			}
			catch (Exception)
			{
				return this.Output<GetFeedBackByFoodIdImageDaoOutputDto>(Constants.ResultCdFail);
			}
		}

	}
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.FeedBackImageDao
{
	public class FeedBackImageDao : BaseDao
	{
		public FeedBackImageDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public List<FeedBackImage>? GetAllImageByFoodId(int feedbackId)
		{
			var output = context.FeedBackImages.Where(
					i => i.FeedbackId == feedbackId
				).ToList();
			if (output.Count > 0)
				return output;
			return null;
		}

		public BaseOutputDto UpdateImageInfor(FeedBackImage image)
		{
			try
			{
				var imageModel = context.FeedBackImages.FirstOrDefault(
					img => img.ImagefeedbackId == image.ImagefeedbackId
				);

				imageModel.Path = image.Path;
				imageModel.FeedbackId = image.FeedbackId;

				context.SaveChanges();

				return Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception)
			{

				return Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public BaseOutputDto AddNewImageFeedBack(FeedBackImage image)
		{
			try
			{
				context.Add(image);
				context.SaveChanges();

				return Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception)
			{

				return Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}
	}
}

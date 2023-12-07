using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.FeedBackCustomer
{
	public class ListFeedBackBL : BaseBusinessLogic
	{
		public ListFeedBackBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		public ListFeedBackOutputDtoBS ListFeedBack(GetFeedBackByFoodIdDaoInputDto input)
		{
			try
			{
				var Dao = this.CreateDao<FeedBackDao>();
				var daooutput = Dao.GetFeedBackByFoodIdImage(input);
				var outputBL = mapper.Map<GetFeedBackByFoodIdImageDaoOutputDto, ListFeedBackOutputDtoBS>(daooutput);// máp
				foreach (var feed in daooutput.FeedBacks)
				{
					// get current index
					var index = daooutput.FeedBacks.IndexOf(feed);

					if (feed.Images == null || feed.Images.Count < 1)
					{
						continue;
					}

					foreach (var img in feed.Images)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;

						imageInfor = ImageFileConvert.ConvertFileToBase64(feed.CustomerId, img.Path, 4);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, FeedImageOutputDto>(imageInfor);
						imageMapper.ImageId = img.ImagefeedbackId;

						// add to ouput list
						outputBL.FeedBacks[index].ImagesBase64.Add(imageMapper);
					}
				}

				return outputBL;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

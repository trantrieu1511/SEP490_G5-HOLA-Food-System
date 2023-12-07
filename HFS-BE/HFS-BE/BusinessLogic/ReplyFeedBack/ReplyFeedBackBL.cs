using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FeedBackReplyDao;
using HFS_BE.DAO.NotificationDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Hosting;

namespace HFS_BE.BusinessLogic.ReplyFeedBack
{
	public class ReplyFeedBackBL : BaseBusinessLogic
	{
		public ReplyFeedBackBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListFeedBackbySellerOutputDtoBL ListFeedBackbySeller(string sellerId)
		{
			try
			{
				var Dao = this.CreateDao<FeedBackReplyDao>();
				var daooutput = Dao.GetFeedBackBySeller(sellerId);
				var outputBL = mapper.Map<ListFeedBackbySellerDaoOutputDto, ListFeedBackbySellerOutputDtoBL>(daooutput);//chưa máp
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
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, FeedSellerImageOutputDto>(imageInfor);
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
		public BaseOutputDto Reply(CreateFeedBackbySellerDaoInputDto input)
		{
			try
			{
				var Dao = this.CreateDao<FeedBackReplyDao>();
				var notifyDao = CreateDao<NotificationDao>();


                var daooutput = Dao.CreateReplyByFeedBackBySeller(input);

				if (!daooutput.Success)
                    return daooutput;

                var notifyBase = GenerateNotification.GetSingleton().GenNotificationReplyFeedBack(input.CustomerId, input.ReplyMessage);
                //3. add notify
                var noti = notifyDao.AddNewNotification(notifyBase);
                if (!noti.Success)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }

                return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.FeedBackCustomer
{
	public class AddFeedBackBL : BaseBusinessLogic
	{
		public AddFeedBackBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public BaseOutputDto AddNewFeedback(AddFeedBackInputDtoBL inputDto)
		{
			try
			{
				var feedDao = CreateDao<FeedBackDao>();
				

				//var feedback = feedDao.CreateFeedBack(inputDto);
			
				var fileNames = new List<string>();
				// save file to server -> return list file name
				if (inputDto.Images != null && inputDto.Images.Count > 0)
					fileNames = ReadSaveImage.SaveImages(inputDto.Images, inputDto.UserDto, 4);

				var inputMapper = mapper.Map<AddFeedBackInputDtoBL, CreateFeedBackDaoInputDto>(inputDto);
				inputMapper.Images = fileNames;

				var output = feedDao.CreateFeedBack(inputMapper);
				if (!output.Success)
					return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Add Failed");

				
				return Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception)
			{
				return this.Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

	}
}

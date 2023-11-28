using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ReplyFeedBack;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.DAO.FeedBackReplyDao;
using HFS_BE.DAO.SellerReportDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.SellerReportBL
{
	public class SellerReportBL : BaseBusinessLogic
	{
		public SellerReportBL(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}

		public BaseOutputDto CreateReport(SellerReportBLInputDto inputDto)
		{
			try
			{
				var report = CreateDao<SellerReportDao>();
				var fileNames = new List<string>();
				// save file to server -> return list file name
				if (inputDto.Images != null && inputDto.Images.Count > 0)
					fileNames = ReadSaveImage.SaveSellerReportImages(inputDto.Images, inputDto.ReportBy, 5);

				var inputMapper = mapper.Map<SellerReportBLInputDto, SellerReportInputDto>(inputDto);
				inputMapper.Images = fileNames;

				var output = report.CreateReportSeller(inputMapper);
				if (!output.Success)
					return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Add Failed");


				return Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public BaseOutputDto UpdateReport(SellerReportByAdminInputDto inputDto)
		{
			try
			{
				var report = CreateDao<SellerReportDao>();
			

				var output = report.UpdateSellerReport(inputDto);
				if (!output.Success)
					return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Add Failed");


				return Output<BaseOutputDto>(Constants.ResultCdSuccess);
			}
			catch (Exception ex)
			{
				return Output<BaseOutputDto>(Constants.ResultCdFail);
			}
		}

		public ListSellerReportOutputDtoBL GetAllSellerReport()
		{
			try
			{
				var report = CreateDao<SellerReportDao>();
				var daooutput = report.GetAllSellerReport();
				var outputBL = mapper.Map<ListSellerReportOutputDto, ListSellerReportOutputDtoBL>(daooutput);//
				foreach (var p in daooutput.data)
				{
					// get current index
					var index = daooutput.data.IndexOf(p);

					if (p.Images == null || p.Images.Count < 1)
					{
						continue;
					}

					foreach (var img in p.Images)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;

						imageInfor = ImageFileConvert.ConvertFileToBase64(p.ReportBy, img.Path, 5);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerReportImageOutputDto>(imageInfor);
						imageMapper.ImageId = img.ImageSellerReportId;

						// add to ouput list
						outputBL.data[index].ImagesBase64.Add(imageMapper);
					}
				}

				return outputBL;
			}
			catch (Exception ex)
			{
				return Output<ListSellerReportOutputDtoBL>(Constants.ResultCdFail);
			}
		}
		public ListSellerReportByCustomerOutputDtoBL GetAllSellerReportByCustomer()
		{
			try
			{
				var report = CreateDao<SellerReportDao>();
				var daooutput = report.GetAllSellerReport();
				var outputBL = mapper.Map<ListSellerReportOutputDto, ListSellerReportByCustomerOutputDtoBL>(daooutput);//
				foreach (var p in daooutput.data)
				{
					// get current index
					var index = daooutput.data.IndexOf(p);

					if (p.Images == null || p.Images.Count < 1)
					{
						continue;
					}

					foreach (var img in p.Images)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;

						imageInfor = ImageFileConvert.ConvertFileToBase64(p.ReportBy, img.Path, 5);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerReportImageOutputDto>(imageInfor);
						imageMapper.ImageId = img.ImageSellerReportId;

						// add to ouput list
						outputBL.data[index].ImagesBase64.Add(imageMapper);
					}
				}

				return outputBL;
			}
			catch (Exception ex)
			{
				return Output<ListSellerReportByCustomerOutputDtoBL>(Constants.ResultCdFail);
			}
		}
	}
}

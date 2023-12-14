using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManageUser.ManageSeller
{
	public class SellerBusinessLogic : BaseBusinessLogic
	{
		public SellerBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListSellerOutputDtoBS ListSeller()
		{
			try
			{
				var Dao = this.CreateDao<SellerDao>();
				var daooutput = Dao.GetAllSeller();
				var outputBL = mapper.Map<ListSellerDtoOutput, ListSellerOutputDtoBS>(daooutput);
				foreach (var seller in daooutput.Sellers)
				{
					// get current index
					var index = daooutput.Sellers.IndexOf(seller);

					if (seller.Images == null || seller.Images.Count < 1)
					{
						continue;
					}

					foreach (var img in seller.Images)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;

						imageInfor = ImageFileConvert.ConvertFileToBase64(seller.SellerId, img.Path, 3);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>(imageInfor);
						imageMapper.ImageId = img.ImageId;

						// add to ouput list
						outputBL.Sellers[index].ImagesBase64.Add(imageMapper);
					}


				}
				foreach (var seller in daooutput.Sellers)
				{
					// get current index
					var index = daooutput.Sellers.IndexOf(seller);

					if (seller.ImagesL == null || seller.ImagesL.Count < 1)
					{
						continue;
					}
					foreach (var img in seller.ImagesL)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;

						imageInfor = ImageFileConvert.ConvertFileToBase64(seller.Email, img.Path, 6);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, SellerImageLOutputDto>(imageInfor);
						imageMapper.ImageId = img.ImageLicenseId;

						// add to ouput list
						outputBL.Sellers[index].ImagesBase64L.Add(imageMapper);
					}
				}

				return outputBL;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto BanSeller(BanSellerDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<SellerDao>();
				var daooutput = Dao.BanSeller(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto ActiveSeller(ActiveSellerDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<SellerDao>();
				var daooutput = Dao.ActiveSeller(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<BaseOutputDto> RejectSeller(RejectSellerDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<SellerDao>();
				var daooutput =await Dao.RejectSeller(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ListHistoryBanSeller ListHistoryBanSeller(BanSellerHistoryDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<SellerDao>();

				var daooutput = Dao.ListHistorySeller(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}

	}
}

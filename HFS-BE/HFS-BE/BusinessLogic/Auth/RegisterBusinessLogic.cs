using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.SellerReportBL;
using HFS_BE.Dao.AuthDao;
using HFS_BE.DAO.AuthDAO;
using HFS_BE.DAO.SellerReportDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.Auth
{
	public class RegisterBusinessLogic : BaseBusinessLogic
	{
		public RegisterBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public async Task<BaseOutputDto> Register(RegisterInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthDao>();
				var daoinput = mapper.Map<RegisterInputDto,RegisterDto>(inputDto);
				var daooutput = await Dao.RegisterCustomer(daoinput);
			

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<BaseOutputDto> RegisterSeller(RegisterSellerInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthNotCustomerDao>();
				var fileNames = new List<string>();
				var daoinput = mapper.Map<RegisterSellerInputDto, RegisterSellerDto>(inputDto);
				if (inputDto.Images != null && inputDto.Images.Count > 0)
					fileNames = ReadSaveImage.SaveSellerReportImages(inputDto.Images, inputDto.Email, 6);

				
				daoinput.Images = fileNames;

				var output = await Dao.RegisterSeller(daoinput);
				

				return output;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public async Task<BaseOutputDto> RegisterShipper(RegisterShipperInputDto inputDto)
		{
			try
			{
				var Dao = this.CreateDao<AuthNotCustomerDao>();
				var fileNames1 = string.Empty;
				var fileNames2 = string.Empty;
				var daoinput = mapper.Map<RegisterShipperInputDto, RegisterShipperDto>(inputDto);
				if (inputDto.Images1 != null)
					fileNames1 = ReadSaveImage.SaveIdCardImages(inputDto.Images1, inputDto.Email, 7);
				if (inputDto.Images2 != null)
					fileNames2 = ReadSaveImage.SaveIdCardImages(inputDto.Images2, inputDto.Email, 7);

				daoinput.IdcardBackImage = fileNames1;
				daoinput.IdcardFrontImage = fileNames2;
				var daooutput = await Dao.RegisterShipper(daoinput);
				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

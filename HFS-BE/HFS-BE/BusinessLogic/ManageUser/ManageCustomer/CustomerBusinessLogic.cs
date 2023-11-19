using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils.IOFile;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;

namespace HFS_BE.BusinessLogic.ManageUser.ManageCustomer
{
	
	public class CustomerBusinessLogic : BaseBusinessLogic
	{
		public CustomerBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		public ListCustomerOutputDtoBS ListCustomer()
		{
			try
			{
				var Dao = this.CreateDao<CustomerDao>();
				var daooutput = Dao.GetAllCustomer();
				var outputBL = mapper.Map<ListCustomerDtoOutput, ListCustomerOutputDtoBS>(daooutput);
				foreach (var cus in daooutput.Customers)
				{
					// get current index
					var index = daooutput.Customers.IndexOf(cus);

					if (cus.Images == null || cus.Images.Count < 1)
					{
						continue;
					}

					foreach (var img in cus.Images)
					{
						ImageFileConvert.ImageOutputDto? imageInfor = null;
						
								imageInfor = ImageFileConvert.ConvertFileToBase64(cus.CustomerId, img.Path, 2);
						if (imageInfor == null)
							continue;
						var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, CustomerImageOutputDto>(imageInfor);
						imageMapper.ImageId = img.ImageId;

						// add to ouput list
						outputBL.Customers[index].ImagesBase64.Add(imageMapper);
					}
				}

				return outputBL;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public BaseOutputDto BanCustomer(BanCustomerDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<CustomerDao>();
				var daooutput = Dao.BanCustomer(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
		public ListHistoryBanCustomer ListHistoryBanCustomer(BanCustomerHistoryDtoInput input)
		{
			try
			{
				var Dao = this.CreateDao<CustomerDao>();
				var daooutput = Dao.ListHistoryCustomer(input);

				return daooutput;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}

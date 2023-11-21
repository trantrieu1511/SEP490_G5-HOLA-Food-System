using HFS_BE.Base;
using HFS_BE.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using HFS_BE.Automapper;
using AutoMapper;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.Dao.ShopDao
{
    public class ShopDao : BaseDao
    {
        public ShopDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }


        public DisplayShopDaoOutputDto DisplayShop()
        {
            try
            {
                var output = this.context.Sellers
                    .Where(x => x.IsVerified == true).ToList();

                DisplayShopDaoOutputDto outputDto = this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdSuccess);
                //output = this.Paginate(output, inputDto.Pagination);
                outputDto.ListShop = mapper.Map<List<Seller>, List<ShopDto>>(output);
                foreach (var item in outputDto.ListShop)
                {
                    item.Avatar = ImageFileConvert.ConvertFileToBase64(item.UserId, this.context.ProfileImages.FirstOrDefault(x => x.UserId.Equals(item.UserId)).Path, 2).ImageBase64;
                }

                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<DisplayShopDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetShopDetailDaoOutputDto GetShopDetail(GetShopDetailDaoInputDto inputDto)
        {
            try
            {
                var output = this.context.Sellers
                    .Where(x => x.SellerId.Equals(inputDto.ShopId) && x.IsVerified == true)
                    .FirstOrDefault();

                var outputDto = this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdSuccess);
                if (output != null)
                {
                    outputDto = mapper.Map<Seller, GetShopDetailDaoOutputDto>(output);
                }
                
                return outputDto;
            }
            catch (Exception ex)
            {
                return this.Output<GetShopDetailDaoOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

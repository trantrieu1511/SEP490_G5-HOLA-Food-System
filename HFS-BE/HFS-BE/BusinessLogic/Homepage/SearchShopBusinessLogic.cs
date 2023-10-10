using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.Homepage
{
    public class SearchShopBusinessLogic : BaseBusinessLogic
    {
        public SearchShopBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public SearchShopOututDto SearchShop(SearchShopBusinessLogicInputDto inputDto)
        {

            ShopDAO Dao = this.CreateDao<ShopDAO>(); 
            SearchShopInputDto input = new SearchShopInputDto();
            input.name = inputDto.Name;
            var output = Dao.SearchShop(input);
            var outputDto = new SearchShopOututDto();
            foreach (var item in output.ListUser)
            {
                UserDto user = new UserDto();
                user.Id = item.Id;
                user.Name = item.Name;
                outputDto.ListUser.Add(user);
            }

            return outputDto;
        }
    }
}

using HFS_BE.Base;
using HFS_BE.DAO.UserDAO;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.Business.ViewShop
{
    public class SearchShopBusinessLogic : BaseBusinessLogic
    {
        public SearchShopBusinessLogic(SEP490_HFSContext context) : base(context)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputDto"></param>
        /// <returns></returns>
        public SearchShopOututDto SearchShop(SearchShopBusinessLogicInputDto inputDto)
        {

            UserDAO dao = this.CreateDao<UserDAO>(); 
            SearchShopInputDto input = new SearchShopInputDto();
            input.name = inputDto.Name;
            var output = dao.SearchShop(input);
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

using HFS_BE.DAO.UserDAO;

namespace HFS_BE.Business.ViewShop
{
    public class SearchShopBusiness
    {
        public SearchShopOututDto SearchShop(SearchShopInputDto inputDto)
        {
            UserDAO dao = new UserDAO();
            DAO.UserDAO.SearchShopInputDto input = new DAO.UserDAO.SearchShopInputDto();
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

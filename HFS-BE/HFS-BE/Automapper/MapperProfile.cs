using AutoMapper;
using HFS_BE.DAO.UserDAO;
using HFS_BE.Models;

namespace HFS_BE.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            Homepage();
        }

        /// <summary>
        /// dataconvert của màn homepage.
        /// </summary>
        public void Homepage()
        {
            CreateMap<User, ShopDto>();
            CreateMap<List<User>, DisplayShopOutputDto>();
            CreateMap<ShopDto, BusinessLogic.Homepage.ShopDto>();
            CreateMap<DisplayShopOutputDto, BusinessLogic.Homepage.DisplayShopOutputDto>();
        }
    }
}

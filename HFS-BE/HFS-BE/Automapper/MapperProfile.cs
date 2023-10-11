using AutoMapper;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.Automapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            Homepage();
            Auth();
            Post();
            Food();
            Order();
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

        public void Auth()
        {
            CreateMap<LoginInPutDto, Dao.AuthDao.AuthDaoInputDto>();
            CreateMap<RegisterDto, Dao.AuthDao.RegisterDto>();
            CreateMap<AuthDaoOutputDto, LoginOutputDto>();
            CreateMap<RegisterInputDto, RegisterDto>();
            //CreateMap<DisplayShopOutputDto, BusinessLogic.Homepage.DisplayShopOutputDto>();
        }

        public void Post()
        {
            CreateMap<Post, Dao.PostDao.PostOutputDto>();
            CreateMap<List<Post>, Dao.PostDao.PostOutputDto>();
        }

        public void Food()
        {
            CreateMap<Food, Dao.FoodDao.FoodOutputDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<FoodImage, Dao.FoodDao.FoodImageDto>();
        }
        public void Order()
        {
            CreateMap<Order, Dao.OrderDao.OrderDaoOutputDto>();
            CreateMap<OrderDetail, Dao.OrderDao.OrderDetailDto>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name));

        }
    }
}

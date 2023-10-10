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
            CreateMap<LoginInPutDto, DAO.AuthDAO.AuthInputDto>();
            CreateMap<RegisterDto, DAO.AuthDAO.RegisterDto>();
            CreateMap<AuthOutputDto, LoginOutputDto>();
            CreateMap<RegisterInputDto, RegisterDto>();
            //CreateMap<DisplayShopOutputDto, BusinessLogic.Homepage.DisplayShopOutputDto>();
        }

        public void Post()
        {
            CreateMap<Post, Dao.PostDao.PostOutputDto>();
            CreateMap<List<Post>, Dao.PostDao.ListPostOutputDto>();
            CreateMap<List<Dao.PostDao.PostOutputDto>, List<BusinessLogic.Post.PostOutputDto>>();
            CreateMap<Dao.PostDao.PostOutputDto, BusinessLogic.Post.PostOutputDto>();
            CreateMap<Dao.PostDao.ListPostOutputDto, BusinessLogic.Post.ListPostOutputDto>();
        }
    }
}

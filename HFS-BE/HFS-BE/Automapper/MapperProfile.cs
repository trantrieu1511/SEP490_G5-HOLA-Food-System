using AutoMapper;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.BusinessLogic.Cart;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Dao.PostDao;
using HFS_BE.Dao.ShopDao;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Components.Forms;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.BusinessLogic.Post;
using HFS_BE.Utils.IOFile;
using Microsoft.AspNetCore.Mvc;
using HFS_BE.Controllers.Food;
using HFS_BE.DAO.OrderProgressDao;

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
            OrderProgress();
            OrderHistory();
            Cart();
            UserProfile();
            File();
        }

        /// <summary>
        /// dataconvert của màn homepage.
        /// </summary>
        public void Homepage()
        {
            CreateMap<User, ShopDto>();
            CreateMap<List<User>, DisplayShopDaoOutputDto>();
            CreateMap<ShopDto, BusinessLogic.Homepage.ShopDto>();
            CreateMap<DisplayShopDaoOutputDto, BusinessLogic.Homepage.DisplayShopOutputDto>();
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

            //seller
            //input
            CreateMap<Dao.PostDao.PostOutputSellerDto, BusinessLogic.Post.PostOutputSellerDto>();
            CreateMap<BusinessLogic.Post.PostCreateInputDto, Dao.PostDao.PostCreateInputDto>();
            CreateMap<Controllers.ManagePost.PostCreateInputDto, BusinessLogic.Post.PostCreateInputDto>();
            //output
            CreateMap<Dao.PostDao.PostOutputSellerDto, BusinessLogic.Post.PostOutputSellerDto>();
            CreateMap<Dao.PostDao.ListPostOutputSellerDto, BusinessLogic.Post.ListPostOutputSellerDto>();

        }

        public void Food()
        {
            CreateMap<Food, Dao.FoodDao.FoodOutputDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<FoodImage, Dao.FoodDao.FoodImageDto>();

            //seller
            //input
            CreateMap<BusinessLogic.Food.FoodCreateInputDto, Dao.FoodDao.FoodCreateInputDto>();
            CreateMap<FoodCreateInputDto, BusinessLogic.Food.FoodCreateInputDto> ();
            //output

        }
        public void Order()
        {
            CreateMap<Order, Dao.OrderDao.OrderDaoOutputDto>();
            CreateMap<OrderDetail, Dao.OrderDao.OrderDetailDto>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name));

            // checkout order
            CreateMap<CheckOutOrderDaoInputDto, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CartItemDaoInputDto, OrderDetail>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Amount));
            CreateMap<ListShopItemDto, CheckOutOrderDaoInputDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ListItem));
            CreateMap<CartItemDto, CartItemDaoInputDto>();

        }
        public void OrderHistory()
        {
            CreateMap<Order, Dao.OrderDao.OrderDaoOutputDto>();
            CreateMap<OrderDetail, Dao.OrderDao.OrderDetailDto>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name));

        }
        public void OrderProgress()
        {
            CreateMap<OrderProgressDaoInputDto, OrderProgress>();
            CreateMap<Order, OrderHistoryDetailDtoOutput>();
            CreateMap<OrderProgress, OrderProgressDaoOutputDto>();

            CreateMap<Controllers.OrderShipper.OrderProgressControllerInputDto, BusinessLogic.OrderShipper.OrderProgressBusinessLogicInputDto>();
            CreateMap<BusinessLogic.OrderShipper.OrderProgressBusinessLogicInputDto,DAO.OrderProgressDao.OrderProgressDaoInputDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserDto.UserId));
        }
        public void Shop()
        {
            CreateMap<User, GetShopDetailDaoOutputDto>()
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.UserId));            
        }

        public void Cart()
        {
            CreateMap<FoodImage, DAO.CartDao.FoodImagesDto>();
            CreateMap<CartItem, DAO.CartDao.CartItemOutputDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.foodImages, opt => opt.MapFrom(src => src.Food.FoodImages))
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.Food.ShopId))
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Food.Shop.ShopName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Food.UnitPrice));
            CreateMap<AddCartItemInputDto, CartItem>();

            CreateMap<CartItemOutputDto, CartItemDto>()
                .ForMember(dest => dest.foodImages, opt => opt.MapFrom(src => src.foodImages.FirstOrDefault()));
        }

        public void UserProfile()
        {
            CreateMap<User, UserProfileOutputDto>();
        }
        
        public void File()
        {
            CreateMap<ImageFileConvert.ImageOutputDto, PostImageOutputSellerDto>();
        }
    }
}

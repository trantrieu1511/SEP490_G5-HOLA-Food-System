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
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.Utils.IOFile;
using Microsoft.AspNetCore.Mvc;
using HFS_BE.Controllers.ManageFood;
using HFS_BE.BusinessLogic.ManageFood;
using HFS_BE.Dao.FoodDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.BusinessLogic.OrderShipper;
using HFS_BE.DAO.ShipperDao;
using HFS_BE.DAO.FeedBackDao;
using HFS_BE.DAO.FeedBackReplyDao;
using HFS_BE.BusinessLogic.FoodDetail;
using HFS_BE.Utils.Enum;
using HFS_BE.Controllers.ManageOrder;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.DAO.ModeratorDao;
using HFS_BE.BusinessLogic.ManageUser;
using HFS_BE.DAO.VoucherDao;

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
            Category();
            FeedBack();
            Shipper();
            Manage();
            Voucher();
        }

        /// <summary>
        /// dataconvert của màn homepage.
        /// </summary>
        public void Homepage()
        {
            //CreateMap<User, ShopDto>();
            //CreateMap<List<User>, DisplayShopDaoOutputDto>();
            CreateMap<ShopDto, BusinessLogic.Homepage.ShopDto>();
            CreateMap<DisplayShopDaoOutputDto, BusinessLogic.Homepage.DisplayShopOutputDto>();
        }

        public void Auth()
        {
            CreateMap<LoginInPutDto, Dao.AuthDao.AuthDaoInputDto>();
            CreateMap<RegisterDto, Dao.AuthDao.RegisterDto>();
            CreateMap<AuthDaoOutputDto, LoginOutputDto>();
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
            CreateMap<Dao.PostDao.PostOutputSellerDto, BusinessLogic.ManagePost.PostOutputSellerDto>();
            CreateMap<BusinessLogic.ManagePost.PostCreateInputDto, Dao.PostDao.PostCreateInputDto>();
            CreateMap<Controllers.ManagePost.PostUpdateInputDto, BusinessLogic.ManagePost.PostUpdateInputDto>();
            CreateMap<BusinessLogic.ManagePost.PostUpdateInputDto, Dao.PostDao.PostUpdateInputDto>();
            //output
            CreateMap<Dao.PostDao.PostOutputSellerDto, BusinessLogic.ManagePost.PostOutputSellerDto>();
            CreateMap<Dao.PostDao.ListPostOutputSellerDto, BusinessLogic.ManagePost.ListPostOutputSellerDto>();

        }

        public void Food()
        {
            CreateMap<Food, Dao.FoodDao.FoodOutputDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<FoodImage, Dao.FoodDao.FoodImageDto>();

            //seller
            //input
            CreateMap<Controllers.ManageFood.FoodCreateInputDto, BusinessLogic.ManageFood.FoodCreateInputDto>();
            CreateMap<BusinessLogic.ManageFood.FoodCreateInputDto, Dao.FoodDao.FoodCreateInputDto>();
            CreateMap<Dao.FoodDao.FoodCreateInputDto, BusinessLogic.ManageFood.FoodCreateInputDto>();
            CreateMap<BusinessLogic.ManageFood.FoodUpdateInputDto, Dao.FoodDao.FoodUpdateInforInputDto>();
            CreateMap<Controllers.ManageFood.FoodUpdateInputDto, BusinessLogic.ManageFood.FoodUpdateInputDto>();
            //output
            CreateMap<Dao.FoodDao.FoodOutputSellerDto, BusinessLogic.ManageFood.FoodOutputSellerDto>();
            CreateMap<Dao.FoodDao.ListFoodOutputSellerDto, BusinessLogic.ManageFood.ListFoodOutputSellerDto>();

        }
        public void Order()
        {
            CreateMap<List<Order>, OrderDaoOutputDto>();
            CreateMap<Order, Dao.OrderDao.OrderDaoOutputDto>();
            CreateMap<OrderDetail, Dao.OrderDao.OrderDetailDto>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Food.SellerId))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Food.FoodImages.AsQueryable().First().Path));
            CreateMap<Dao.OrderDao.OrderDetailDto, BusinessLogic.OrderShipper.OrderDetailBLDto>();
            CreateMap<Dao.OrderDao.OrderDaoOutputDto, BusinessLogic.OrderShipper.OrderBLOutputDto>();
            CreateMap<Dao.OrderDao.OrderByShipperDaoOutputDto, BusinessLogic.OrderShipper.OrderByShipperBLOutputDto>();
            CreateMap<Dao.OrderDao.OrderHistoryDaoOutputDto, OrderByShipperBLOutputDto>();


            // checkout order
            CreateMap<CheckOutOrderDaoInputDto, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.Items))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<CartItemDaoInputDto, OrderDetail>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Amount));
            CreateMap<ListShopItemInputDto, CheckOutOrderDaoInputDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
            CreateMap<CartItemDto, CartItemDaoInputDto>();
            CreateMap<CartItemInputDto, CartItemDaoInputDto>();


            //seller
            //*input
            CreateMap<OrderCancelInputDto, OrderProgressCancelInputDto>();
            CreateMap<OrderAcceptInputDto, OrderProgressStatusInputDto>();
            CreateMap<OrderInternalShipInputDto, OrderProgressStatusInputDto>();
            CreateMap<OrderInternalInputDto, OrderInternalShipInputDto>();
            //*output

            //CreateMap<List<Order>, OrderDaoSellerOutputDto>();
            CreateMap<Order, Dao.OrderDao.OrderDaoSellerOutputDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FirstName + " " + src.Customer.LastName : null))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate != null ? src.OrderDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate != null ? src.RequiredDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate != null ? src.ShippedDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.Shipper != null ? src.Shipper.FirstName + " " + src.Shipper.LastName : null))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => PaymentMethodEnum.GetPaymentMethodString(src.PaymentMethod)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Select(d => d.UnitPrice * d.Quantity).ToList().Sum())) //* them voucher))
                .ForMember(dest => dest.OrderProgresses, opt => opt.MapFrom(src => src.OrderProgresses.OrderBy(x => x.CreateDate).ToList()))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails.OrderBy(x => x.UnitPrice).ToList()));
            /*.ForMember(dest => dest.Detail, opt => opt.MapFrom(src => new DetailProgress
                {
                    Image = src.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Image,
                    Note = src.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().Note,
                    CreateDate = src.OrderProgresses.OrderBy(x => x.CreateDate).AsQueryable().Last().CreateDate.Value.ToString("MM/dd/yyyy - HH:mm:ss")
                }))
            .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails.Select(od => new OrderDetailFoodDto
                {
                    OrderId = od.OrderId,
                    FoodId = od.FoodId,
                    FoodName = od.Food.Name,
                    UnitPrice = od.UnitPrice,
                    Quantity = od.Quantity,
                    Image = od.Food.FoodImages.ToList().First().Path,
                    CategoryName = od.Food.Category.Name
                }).ToList()));*/

            CreateMap<OrderProgress, DetailProgress>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.Value.ToString("MM/dd/yyyy - HH:mm:ss")));
            //CreateMap<ICollection<OrderProgress>, List<DetailProgress>>();

            CreateMap<OrderDetail, OrderDetailFoodDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.FoodId, opt => opt.MapFrom(src => src.FoodId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Food.FoodImages.ToList().First().Path))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Food.Category.Name))
                .ForMember(dest => dest.SellId, opt => opt.MapFrom(src => src.Food.SellerId));
            //CreateMap<ICollection<OrderDetail>, List<OrderDetailFoodDto>>();

            CreateMap<Dao.OrderDao.OrderDetailFoodDto, BusinessLogic.ManageOrder.OrderDetailFoodDto>();
            CreateMap<Dao.OrderDao.DetailProgress, BusinessLogic.ManageOrder.DetailProgress>();
            CreateMap<Dao.OrderDao.OrderDaoSellerOutputDto, BusinessLogic.ManageOrder.OrderDaoSellerOutputDto>();
            CreateMap<Dao.OrderDao.OrderSellerDaoOutputDto, BusinessLogic.ManageOrder.OrderSellerDaoOutputDto>();

            // ordercustomer
            CreateMap<Order, Dao.OrderDao.OrderCustomerDaoOutputDto>()
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Seller != null ? src.Seller.ShopName : null))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate != null ? src.OrderDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate != null ? src.RequiredDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate != null ? src.ShippedDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.Shipper != null ? src.Shipper.FirstName + " " + src.Shipper.LastName : null))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => PaymentMethodEnum.GetPaymentMethodString(src.PaymentMethod)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Select(d => d.UnitPrice * d.Quantity).ToList().Sum())) //* them voucher))
                .ForMember(dest => dest.OrderProgresses, opt => opt.MapFrom(src => src.OrderProgresses.OrderBy(x => x.CreateDate).ToList()))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails.OrderBy(x => x.UnitPrice).ToList()));
            CreateMap<OrderProgress, DetailProgressCustomerDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.Value.ToString("MM/dd/yyyy - HH:mm:ss")));
            CreateMap<OrderDetail, OrderDetaiCustomerDto>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Food.FoodImages.ToList().First().Path))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Food.Category.Name));

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
            CreateMap<BusinessLogic.OrderShipper.OrderProgressBusinessLogicInputDto, DAO.OrderProgressDao.OrderProgressDaoInputDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserDto.UserId));


            // check-out customer
            CreateMap<OrderCreateDaoInputDto, OrderProgress>();
        }
        public void Shop()
        {
            CreateMap<Seller, GetShopDetailDaoOutputDto>()
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.SellerId));
        }

        public void Cart()
        {
            CreateMap<FoodImage, DAO.CartDao.FoodImagesDto>();
            CreateMap<CartItem, DAO.CartDao.CartItemOutputDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.foodImages, opt => opt.MapFrom(src => src.Food.FoodImages))
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.Food.SellerId))
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Food.Seller.ShopName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Food.UnitPrice));
            CreateMap<AddCartItemInputDto, CartItem>();

            CreateMap<CartItemOutputDto, CartItemDto>()
                .ForMember(dest => dest.foodImages, opt => opt.MapFrom(src => src.foodImages.FirstOrDefault()));
        }

        public void UserProfile()
        {
            CreateMap<Admin, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.AdminId));
            CreateMap<Customer, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CustomerId));
            CreateMap<Seller, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.SellerId));
            CreateMap<Shipper, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ShipperId));
            CreateMap<PostModerator, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ModId));
            CreateMap<MenuModerator, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ModId));
            //.ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(Convert.ToDateTime(src.BirthDate))));
        }

        public void File()
        {
            CreateMap<ImageFileConvert.ImageOutputDto, PostImageOutputSellerDto>();
            CreateMap<ImageFileConvert.ImageOutputDto, FoodImageOutputSellerDto>();
            CreateMap<ImageFileConvert.ImageOutputDto, BusinessLogic.OrderShipper.ImageFoodOutputDto>();
        }

        public void Category()
        {
            CreateMap<CategoryDaoInputDto, Category>();
            CreateMap<Category, CategoryDaoOutputDto>();
        }

        public void Shipper()
        {
            CreateMap<Shipper, ShipperInfor>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate != null ? src.BirthDate.Value.ToString("MM/dd/yyyy") : null))
                .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
        public void Manage()
        {
            CreateMap<Customer, CustomerDtoOutput>();
			CreateMap<Seller, SellerDtoOutput>();
			CreateMap<PostModerator, PostModeratorDtoOutput>();
			CreateMap<MenuModerator, MenuModeratorDtoOutput>();
			CreateMap<MenuModerator, MenuModeratorDtoOutput>();
			CreateMap<CreateModerator, CreateModeratorDaoDtoInput>();
     
		}
	
        public void FeedBack()
        {
            CreateMap<Feedback, FeedBackDaoOutputDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName + " " + src.Customer.LastName))
                .ForMember(dest => dest.DisplayDate, opt => opt.MapFrom(src => src.UpdateDate ?? src.CreatedDate))
                .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.FeedbackVotes.Where(x => x.IsLike == true).ToList().Count))
                .ForMember(dest => dest.DisLikeCount, opt => opt.MapFrom(src => src.FeedbackVotes.Where(x => x.IsLike == false).ToList().Count))
                .ForMember(dest => dest.ListVoted, opt => opt.MapFrom(src => src.FeedbackVotes));
            CreateMap<FeedbackVote, CustomerVoted>();
            CreateMap<FeedbackReply, FeedBackReplyDaoOutputDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FirstName + " " + src.Customer.LastName : src.Seller.ShopName));


            CreateMap<GetFeedBackByFoodIdDaoOutputDto, GetFeedBackOutputDto>();
            CreateMap<FeedBackDaoOutputDto, FeedBackOutputDto>();
            CreateMap<FeedBackReplyDaoOutputDto, FeedBackReplyOutputDto>();
        }

        public void Voucher()
        {
            CreateMap<Voucher, GetVoucherDaoOutputDto>();

        }

    }
}


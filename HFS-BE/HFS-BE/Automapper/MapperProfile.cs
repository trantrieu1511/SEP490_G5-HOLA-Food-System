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
using HFS_BE.DAO.NotificationDao;
using HFS_BE.DAO.PostReportDao;
using HFS_BE.DAO.ChatMessageDao;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.BusinessLogic.ManageUser.ManageCustomer;
using HFS_BE.BusinessLogic.ManageUser.ManageSeller;
using HFS_BE.DAO.ShipAddressDao;
using HFS_BE.DAO.TransantionDao;
using static HFS_BE.Utils.Enum.CategoryStatusEnum;

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
            ProfileImage();
            File();
            Category();
            FeedBack();
            Shipper();
            Manage();
            Voucher();
            Shop();
            Notification();
            Chat();
            Google();
            Comment();
            ShipAddress();
            Transaction();
        }

        /// <summary>
        /// dataconvert của màn homepage.
        /// </summary>
        public void Homepage()
        {
            CreateMap<Seller, ShopDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(x => x.SellerId));
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
			CreateMap<RegisterSellerInputDto, RegisterSellerDto>();
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
            CreateMap<Dao.PostDao.PostByCustomerOutputDto, BusinessLogic.ManagePost.PostOutputCustomerDto>();
            CreateMap<Dao.PostDao.ListPostByCustomerOutputDto, BusinessLogic.ManagePost.ListPostOutputCustomerDto>();
        }

        public void Food()
        {
            CreateMap<Food, Dao.FoodDao.FoodOutputDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<FoodImage, Dao.FoodDao.FoodImageDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Path));

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
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.ShopId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 0));
            CreateMap<CartItemDaoInputDto, OrderDetail>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Amount));
            CreateMap<ListShopItemInputDto, CheckOutOrderDaoInputDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.CartItems));
            CreateMap<CartItemDto, CartItemDaoInputDto>();
            CreateMap<CartItemInputDto, CartItemDaoInputDto>();
            CreateMap<Order, OrderExternalShipperOutputDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FirstName + " " + src.Customer.LastName : null))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate != null ? src.OrderDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => PaymentMethodEnum.GetPaymentMethodString(src.PaymentMethod)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Select(
                        d => d.UnitPrice * d.Quantity
                    ).ToList().Sum() - (src.Voucher != null ? src.Voucher.DiscountAmount : 0))) //* them voucher))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails.OrderBy(x => x.UnitPrice).ToList()));

            //seller
            //*input
            CreateMap<OrderCancelInputDto, OrderProgressCancelInputDto>();
            CreateMap<OrderAcceptInputDto, OrderProgressStatusInputDto>();
            CreateMap<OrderInternalShipInputDto, OrderProgressStatusInputDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId));
            CreateMap<OrderInternalInputDto, OrderInternalShipInputDto>();
            CreateMap<OrderExternalShipInputDto, OrderProgressStatusInputDto>();
            CreateMap<OrderExternalInputDto, OrderExternalShipInputDto>();
            //*output

            //CreateMap<List<Order>, OrderDaoSellerOutputDto>();
            CreateMap<Order, Dao.OrderDao.OrderDaoSellerOutputDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? src.Customer.FirstName + " " + src.Customer.LastName : null))
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate != null ? src.OrderDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.RequiredDate, opt => opt.MapFrom(src => src.RequiredDate != null ? src.RequiredDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.ShippedDate, opt => opt.MapFrom(src => src.ShippedDate != null ? src.ShippedDate.Value.ToString("MM/dd/yyyy - HH:mm:ss") : null))
                .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.Shipper != null ? src.Shipper.FirstName + " " + src.Shipper.LastName : null))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => PaymentMethodEnum.GetPaymentMethodString(src.PaymentMethod)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Select(
                        d => d.UnitPrice * d.Quantity
                    ).ToList().Sum() - (src.Voucher != null ? src.Voucher.DiscountAmount : 0))) //* them voucher))
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

            CreateMap<OrderDetail, Dao.OrderDao.OrderDetailFoodDto>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.FoodId, opt => opt.MapFrom(src => src.FoodId))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Food.FoodImages.ToList().First().Path))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Food.Category.Name))
                .ForMember(dest => dest.SellId, opt => opt.MapFrom(src => src.Food.SellerId));
            //CreateMap<ICollection<OrderDetail>, List<OrderDetailFoodDto>>();

            CreateMap<Dao.OrderDao.OrderDetailFoodDto, BusinessLogic.ManageOrder.OrderDetailFoodBLDto>();
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
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.OrderDetails.Select(
                        d => d.UnitPrice * d.Quantity
                    ).ToList().Sum() - (src.Voucher != null ? src.Voucher.DiscountAmount : 0))) //* them voucher))
                .ForMember(dest => dest.OrderProgresses, opt => opt.MapFrom(src => src.OrderProgresses.OrderBy(x => x.CreateDate).ToList()))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails.OrderBy(x => x.UnitPrice).ToList()));
            CreateMap<OrderProgress, DetailProgressCustomerDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.Value.ToString("MM/dd/yyyy - HH:mm:ss")));
            CreateMap<OrderDetail, OrderDetaiCustomerDto>()
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => ImageFileConvert.ConvertFileToBase64(src.Food.SellerId, src.Food.FoodImages.ToList().First().Path, 1).ImageBase64))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Food.Category.Name));

        }
        public void OrderHistory()
        {
            CreateMap<Order, Dao.OrderDao.OrderDaoOutputDto>()
                .ForMember(dest => dest.OrderProgresses, opt => opt.MapFrom(src => src.OrderProgresses))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
            CreateMap<OrderProgress, Dao.OrderDao.OrderProgressDto>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.SellerId ?? src.CustomerId ?? src.ShipperId));
            CreateMap<OrderDetail, Dao.OrderDao.OrderDetailDto>()
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Food.FoodImages.AsQueryable().First().Path))
                .ForMember(dest => dest.FoodName, opt => opt.MapFrom(src => src.Food.Name))
                .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.Food.SellerId));


        }
        public void OrderProgress()
        {
            CreateMap<OrderProgressDaoInputDto, OrderProgress>();
            CreateMap<Order, OrderHistoryDetailDtoOutput>();
            CreateMap<OrderProgress, OrderProgressDaoOutputDto>();

            CreateMap<Controllers.OrderShipper.OrderProgressControllerInputDto, BusinessLogic.OrderShipper.OrderProgressBusinessLogicInputDto>();
            CreateMap<BusinessLogic.OrderShipper.OrderProgressBusinessLogicInputDto, DAO.OrderProgressDao.OrderProgressDaoInputDto>()
                .ForMember(dest => dest.ShipperId, opt => opt.MapFrom(src => src.UserDto.UserId));


            // check-out customer
            CreateMap<OrderCreateDaoInputDto, OrderProgress>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId));
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
                .ForMember(dest => dest.foodImages, opt => opt.MapFrom(src => ImageFileConvert.ConvertFileToBase64(src.Food.SellerId, src.Food.FoodImages.FirstOrDefault().Path, 1).ImageBase64))
                .ForMember(dest => dest.ShopId, opt => opt.MapFrom(src => src.Food.SellerId))
                .ForMember(dest => dest.ShopName, opt => opt.MapFrom(src => src.Food.Seller.ShopName))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.Food.UnitPrice));
            CreateMap<AddCartItemInputDto, CartItem>();

            CreateMap<CartItemOutputDto, CartItemDto>()
                .ForMember(dest => dest.foodImages, opt => opt.MapFrom(src => src.foodImages));
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

        public void ProfileImage()
        {
            CreateMap<BusinessLogic.ProfileImage.ProfileImageInputDto, DAO.ProfileImage.ProfileImageInputDto>();
            CreateMap<DAO.ProfileImage.ProfileImageOutputDto, BusinessLogic.ProfileImage.ProfileImageOutputDto>();
            CreateMap<DAO.ProfileImage.ProfileImageOutputDtoWrapper, BusinessLogic.ProfileImage.ProfileImageOutputDtoWrapper>();
        }

        public void File()
        {
            CreateMap<ImageFileConvert.ImageOutputDto, PostImageOutputCustomerDto>();
            CreateMap<ImageFileConvert.ImageOutputDto, PostImageOutputSellerDto>();
            CreateMap<ImageFileConvert.ImageOutputDto, FoodImageOutputSellerDto>();
            CreateMap<ImageFileConvert.ImageOutputDto, BusinessLogic.OrderShipper.ImageFoodOutputDto>();

            CreateMap<ImageFileConvert.ImageOutputDto, CustomerImageOutputDto>();
            CreateMap<ImageFileConvert.ImageOutputDto, SellerImageOutputDto>();
        }

        public void Category()
        {
            CreateMap<Category, CategoryDaoOutputDto>();
            CreateMap<Category, GetCategoryOutputDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src=>CategoryStatusEnum.GetStatusString(src.Status)));
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
            CreateMap<CustomerBan, BanHistoryCustomerDtoOutput>();
            CreateMap<Invitation, InvitationShipperDtoOutput>()
                .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.Shipper.FirstName + " " + src.Shipper.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Shipper.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Shipper.PhoneNumber));
            //.ForMember(dest => dest.Avatar, opt => opt.MapFrom(src => src.Shipper.Avatar));
            CreateMap<Shipper, ShipperInforByAdmin>()
        .ForMember(dest => dest.ShipperName, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
            CreateMap<SellerBan, BanHistorySellerDtoOutput>();
            CreateMap<ShipperBan, BanHistoryShipperDtoOutput>();
            CreateMap<CustomerDtoOutput, CustomerDtoBS>();
            CreateMap<ListCustomerDtoOutput, ListCustomerOutputDtoBS>();
            CreateMap<SellerDtoOutput, SellerDtoBS>();
            CreateMap<ListSellerDtoOutput, ListSellerOutputDtoBS>();
			CreateMap<Invitation, InvitationSellerDtoOutput>()
			 .ForMember(dest => dest.SellerName, opt => opt.MapFrom(src => src.Seller.FirstName + " " + src.Seller.LastName))
			 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Seller.Email))
			 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Seller.PhoneNumber));
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
            CreateMap<Voucher, GetVoucherDaoOutputDto>()
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => VoucherStatusEnum.GetStatusString(src.Status)));



        }

        public void Notification()
        {
            CreateMap<NotificationAddNewInputDto, Notification>();
            CreateMap<Notification, NotificationDaoOutputDto>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => NotificationTypeEnum.GetNotifyString(src.Type)))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => src.CreateDate.Value.ToString("MM/dd/yyyy hh:mm:ss tt")));
        }
        public void Chat()
        {
            CreateMap<ChatMessage, MessageDtoOuput>()
                .ForMember(dest => dest.EmailCustomer, opt => opt.MapFrom(src => src.Customer.Email))
                 .ForMember(dest => dest.EmailSeller, opt => opt.MapFrom(src => src.Seller.Email));
            CreateMap<Seller, SellerMessageDtoOutput>();
            CreateMap<Customer, CustomerMessageDtoOutput>()
                ;
        }
        public void Google()
        {
            CreateMap<Seller, LoginGoogleInputDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.SellerId));
            CreateMap<Customer, LoginGoogleInputDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CustomerId));
            CreateMap<PostModerator, LoginGoogleInputDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ModId));
            CreateMap<MenuModerator, LoginGoogleInputDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ModId));
        }



        public void Comment()
        {
            CreateMap<Comment, CommentOutputDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.FirstName +" "+ src.Customer.LastName));
            CreateMap<CommentOutputDto, Customer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.CustomerName));

        }

        private void ShipAddress()
        {
            CreateMap<ShipAddress, ShipAddressOutputDto>();
            //CreateMap<List<ShipAddress>, List<ShipAddressOutputDto>>();
        }

        public void Transaction()
        {
            CreateMap<TransactionHistory, GetTransactionHistoryDaoDto>();
        }
    }
}



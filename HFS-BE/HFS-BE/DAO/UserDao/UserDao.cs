using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.UserDao
{
    /// <summary>
    /// The root class of all users (Customer, admin, seller, shipper, post/menu moderator)
    /// </summary>
    public class UserDao : BaseDao
    {
        public UserDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        /// <summary>
        /// Get the user's profile by user's id
        /// </summary>
        /// <param name="userId">The id of the user when they login</param>
        /// <returns>UserProfileOutputDto, which is the response of this function to the user 
        /// that consists of the user's profile, messages and success status </returns>
        public UserProfileOutputDto GetUserProfileById(string userId)
        {
            string userRole = userId.Substring(0, 2); // Lay role key: CU/SE/SH/PM/MM/AD
            UserProfile? dataMapper = null;
            try
            {
                switch (userRole)
                {
                    case "CU":
                        var customer = context.Customers.SingleOrDefault(cu => cu.CustomerId.Equals(userId));
                        if (customer == null)
                            return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                        dataMapper = mapper.Map<Customer, UserProfile>(customer);
                        break;
                    case "SE":
                        var seller = context.Sellers.SingleOrDefault(se => se.SellerId.Equals(userId));
                        if (seller == null)
                            return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                        dataMapper = mapper.Map<Seller, UserProfile>(seller);
                        break;
                    case "SH":
                        var shipper = context.Shippers.SingleOrDefault(sh => sh.ShipperId.Equals(userId));
                        if (shipper == null)
                            return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                        dataMapper = mapper.Map<Shipper, UserProfile>(shipper);
                        break;
                    case "AD":
                        var admin = context.Admins.SingleOrDefault(ad => ad.AdminId.Equals(userId));
                        if (admin == null)
                            return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                        dataMapper = mapper.Map<Admin, UserProfile>(admin);
                        break;
                    case "PM":
                        var postModerator = context.PostModerators.SingleOrDefault(pm => pm.ModId.Equals(userId));
                        if (postModerator == null)
                            return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                        dataMapper = mapper.Map<PostModerator, UserProfile>(postModerator);
                        break;
                    case "MM":
                        var menuModerator = context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(userId));
                        if (menuModerator == null)
                            return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                        dataMapper = mapper.Map<MenuModerator, UserProfile>(menuModerator);
                        break;
                    default:
                        return Output<UserProfileOutputDto>(Constants.ResultCdFail, "Some error occured. Debug BE for more info.");
                }
                var output = Output<UserProfileOutputDto>(Constants.ResultCdSuccess);
                output.data = dataMapper;
                return output;
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }

        /// <summary>
        /// Enable user to update their personal information including: First, last name, gender and birth date
        /// </summary>
        /// <param name="inputDto">The input of user</param>
        /// <returns>BaseOutputDto, which is the response of this function to the user that 
        /// consists of messages and success status</returns>
        public BaseOutputDto EditProfileById(EditUserProfileInputDto inputDto)
        {
            string userRole = inputDto.UserId.Substring(0, 2); // Lay role key: CU/SE/SH/PM/MM/AD
            try
            {
                switch (userRole)
                {
                    case "CU":
                        //Tim trong context profile cua user theo id
                        var customer = context.Customers.SingleOrDefault(
                        cu => cu.CustomerId.Equals(inputDto.UserId));

                        //Check user co ton tai hay khong
                        if (customer == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        customer.FirstName = inputDto.FirstName;
                        customer.LastName = inputDto.LastName;
                        customer.Gender = inputDto.Gender;
                        customer.BirthDate = inputDto.BirthDate;

                        break;
                    case "SE":
                        //Tim trong context profile cua user theo id
                        var seller = context.Sellers.SingleOrDefault(
                        se => se.SellerId.Equals(inputDto.UserId));

                        //Check user co ton tai hay khong
                        if (seller == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        seller.FirstName = inputDto.FirstName;
                        seller.LastName = inputDto.LastName;
                        seller.Gender = inputDto.Gender;
                        seller.BirthDate = inputDto.BirthDate;

                        break;
                    case "SH":
                        //Tim trong context profile cua user theo id
                        var shipper = context.Shippers.SingleOrDefault(
                        sh => sh.ShipperId.Equals(inputDto.UserId));

                        //Check user co ton tai hay khong
                        if (shipper == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        shipper.FirstName = inputDto.FirstName;
                        shipper.LastName = inputDto.LastName;
                        shipper.Gender = inputDto.Gender;
                        shipper.BirthDate = inputDto.BirthDate;

                        break;
                    case "AD":
                        //Tim trong context profile cua user theo id
                        var admin = context.Admins.SingleOrDefault(
                        ad => ad.AdminId.Equals(inputDto.UserId));

                        //Check user co ton tai hay khong
                        if (admin == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        admin.FirstName = inputDto.FirstName;
                        admin.LastName = inputDto.LastName;
                        admin.Gender = inputDto.Gender;
                        admin.BirthDate = inputDto.BirthDate;

                        break;
                    case "PM":
                        //Tim trong context profile cua user theo id
                        var postModerator = context.PostModerators.SingleOrDefault(
                        pm => pm.ModId.Equals(inputDto.UserId));

                        //Check user co ton tai hay khong
                        if (postModerator == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        postModerator.FirstName = inputDto.FirstName;
                        postModerator.LastName = inputDto.LastName;
                        postModerator.Gender = inputDto.Gender;
                        postModerator.BirthDate = inputDto.BirthDate;

                        break;
                    case "MM":
                        //Tim trong context profile cua user theo id
                        var menuModerator = context.MenuModerators.SingleOrDefault(
                        mm => mm.ModId.Equals(inputDto.UserId));

                        //Check user co ton tai hay khong
                        if (menuModerator == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        menuModerator.FirstName = inputDto.FirstName;
                        menuModerator.LastName = inputDto.LastName;
                        menuModerator.Gender = inputDto.Gender;
                        menuModerator.BirthDate = inputDto.BirthDate;

                        break;
                    default:
                        return Output<UserProfileOutputDto>(Constants.ResultCdFail, "Some error occured. Debug BE for more info.");
                }
                //Luu thay doi trong context vao db
                context.SaveChanges();

                //Output ra response body trang thai thanh cong
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                //Output ra response body trang thai that bai neu co loi/ngoai le bat ky
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        //public GetOrderInfoOutputDto GetUserInfo(GetOrderInfoInputDto inputDto)
        //{
        //    try
        //    {
        //        var user = this.context.Users.Include(x => x.ShipAddresses).FirstOrDefault(x => x.UserId == inputDto.UserId);
        //        var output = this.Output<GetOrderInfoOutputDto>(Constants.ResultCdSuccess);
        //        if (user != null)
        //        {
        //            output.Balance = user.WalletBalance == null ? 0 : user.WalletBalance.Value;
        //            output.Address = user.ShipAddresses.FirstOrDefault() == null ? "" : user.ShipAddresses.FirstOrDefault().AddressInfo;
        //        }

        //        return output;
        //    }
        //    catch (Exception)
        //    {
        //        return this.Output<GetOrderInfoOutputDto>(Constants.ResultCdFail);
        //    }
        //}
    }
}

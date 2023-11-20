using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static HFS_BE.BusinessLogic.Auth.RegisterSellerInputDto;

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
        public BaseOutputDto EditProfileById(EditUserProfileInputDto inputDto, string? userId)
        {
            string userRole = userId.Substring(0, 2); // Lay role key: CU/SE/SH/PM/MM/AD
            try
            {
                switch (userRole)
                {
                    case "CU":
                        //Tim trong context profile cua user theo id
                        var customer = context.Customers.SingleOrDefault(
                        cu => cu.CustomerId.Equals(userId));

                        //Check user co ton tai hay khong
                        if (customer == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {userId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        customer.FirstName = inputDto.FirstName;
                        customer.LastName = inputDto.LastName;
                        customer.Gender = inputDto.Gender;
                        customer.BirthDate = inputDto.BirthDate;
                        customer.PhoneNumber = inputDto.PhoneNumber;

                        break;
                    case "SE":
                        //Tim trong context profile cua user theo id
                        var seller = context.Sellers.SingleOrDefault(
                        se => se.SellerId.Equals(userId));

                        //Check user co ton tai hay khong
                        if (seller == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {userId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        seller.FirstName = inputDto.FirstName;
                        seller.LastName = inputDto.LastName;
                        seller.Gender = inputDto.Gender;
                        seller.BirthDate = inputDto.BirthDate;
                        seller.ShopName = inputDto.ShopName;
                        seller.ShopAddress = inputDto.ShopAddress;
                        seller.PhoneNumber = inputDto.PhoneNumber;

                        break;
                    case "SH":
                        //Tim trong context profile cua user theo id
                        var shipper = context.Shippers.SingleOrDefault(
                        sh => sh.ShipperId.Equals(userId));

                        //Check user co ton tai hay khong
                        if (shipper == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {userId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        shipper.FirstName = inputDto.FirstName;
                        shipper.LastName = inputDto.LastName;
                        shipper.Gender = inputDto.Gender;
                        shipper.BirthDate = inputDto.BirthDate;
                        shipper.PhoneNumber = inputDto.PhoneNumber;

                        break;
                    case "AD":
                        //Tim trong context profile cua user theo id
                        var admin = context.Admins.SingleOrDefault(
                        ad => ad.AdminId.Equals(userId));

                        //Check user co ton tai hay khong
                        if (admin == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {userId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        admin.FirstName = inputDto.FirstName;
                        admin.LastName = inputDto.LastName;
                        admin.Gender = inputDto.Gender;
                        admin.BirthDate = inputDto.BirthDate;
                        admin.PhoneNumber = inputDto.PhoneNumber;

                        break;
                    case "PM":
                        //Tim trong context profile cua user theo id
                        var postModerator = context.PostModerators.SingleOrDefault(
                        pm => pm.ModId.Equals(userId));

                        //Check user co ton tai hay khong
                        if (postModerator == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {userId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        postModerator.FirstName = inputDto.FirstName;
                        postModerator.LastName = inputDto.LastName;
                        postModerator.Gender = inputDto.Gender;
                        postModerator.BirthDate = inputDto.BirthDate;
                        postModerator.PhoneNumber = inputDto.PhoneNumber;

                        break;
                    case "MM":
                        //Tim trong context profile cua user theo id
                        var menuModerator = context.MenuModerators.SingleOrDefault(
                        mm => mm.ModId.Equals(userId));

                        //Check user co ton tai hay khong
                        if (menuModerator == null)
                            return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {userId} is not exist!");

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        menuModerator.FirstName = inputDto.FirstName;
                        menuModerator.LastName = inputDto.LastName;
                        menuModerator.Gender = inputDto.Gender;
                        menuModerator.BirthDate = inputDto.BirthDate;
                        menuModerator.PhoneNumber = inputDto.PhoneNumber;

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

        /// <summary>
        /// Verify the user's identity before continuing to change password
        /// </summary>
        /// <param name="inputDto">Contains password inputted by user</param>
        /// <param name="userId">Id of the user</param>
        /// <returns>BaseOutputDto</returns>
        public BaseOutputDto VerifyUsersIdentity(VerifyIdentityInputDto inputDto, string userId)
        {
            try
            {
                bool result = false;
                switch (userId.Substring(0, 2))
                {
                    case "CU":
                        var customer = context.Customers.SingleOrDefault(c => c.CustomerId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256(customer.PasswordSalt))
                        {
                            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.Password));
                            result = compute.SequenceEqual(customer.PasswordHash);
                        }
                        break;
                    case "SE":
                        var seller = context.Sellers.SingleOrDefault(s => s.SellerId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256(seller.PasswordSalt))
                        {
                            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.Password));
                            result = compute.SequenceEqual(seller.PasswordHash);
                        }
                        break;
                    case "SH":
                        var shipper = context.Shippers.SingleOrDefault(sh => sh.ShipperId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256(shipper.PasswordSalt))
                        {
                            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.Password));
                            result = compute.SequenceEqual(shipper.PasswordHash);
                        }
                        break;
                    case "AD":
                        var admin = context.Admins.SingleOrDefault(ad => ad.AdminId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256(admin.PasswordSalt))
                        {
                            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.Password));
                            result = compute.SequenceEqual(admin.PasswordHash);
                        }
                        break;
                    case "PM":
                        var postMod = context.PostModerators.SingleOrDefault(pm => pm.ModId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256(postMod.PasswordSalt))
                        {
                            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.Password));
                            result = compute.SequenceEqual(postMod.PasswordHash);
                        }
                        break;
                    case "MM":
                        var menuMod = context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256(menuMod.PasswordSalt))
                        {
                            var compute = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.Password));
                            result = compute.SequenceEqual(menuMod.PasswordHash);
                        }
                        break;
                    default:
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "There are some errors happened in the system. Debug for more info.");
                }
                if (!result)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Password is not match");
                }
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);

            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }

        }

        /// <summary>
        /// Change the user's account password
        /// </summary>
        /// <param name="inputDto">Contains new password and confirm password</param>
        /// <param name="userId">The id of the user</param>
        /// <returns></returns>
        public BaseOutputDto ChangeUserAccountPassword(ChangeUserAccountPasswordInputDto inputDto, string userId)
        {
            try
            {
                if (!inputDto.ConfirmPassword.Equals(inputDto.NewPassword))
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Confirm password is not match with the new password. Please try again.");
                }
                switch (userId.Substring(0, 2)) // get rolekey
                {
                    case "CU":
                        var customer = context.Customers.SingleOrDefault(c => c.CustomerId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256())
                        {
                            customer.PasswordSalt = hmac.Key;
                            customer.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.NewPassword));
                        }
                        break;
                    case "SE":
                        var seller = context.Sellers.SingleOrDefault(s => s.SellerId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256())
                        {
                            seller.PasswordSalt = hmac.Key;
                            seller.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.NewPassword));
                        }
                        break;
                    case "SH":
                        var shipper = context.Shippers.SingleOrDefault(sh => sh.ShipperId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256())
                        {
                            shipper.PasswordSalt = hmac.Key;
                            shipper.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.NewPassword));
                        }
                        break;
                    case "AD":
                        var admin = context.Admins.SingleOrDefault(a => a.AdminId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256())
                        {
                            admin.PasswordSalt = hmac.Key;
                            admin.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.NewPassword));
                        }
                        break;
                    case "PM":
                        var postMod = context.PostModerators.SingleOrDefault(pm => pm.ModId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256())
                        {
                            postMod.PasswordSalt = hmac.Key;
                            postMod.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.NewPassword));
                        }
                        break;
                    case "MM":
                        var menuMod = context.MenuModerators.SingleOrDefault(mm => mm.ModId.Equals(userId));
                        using (HMACSHA256? hmac = new HMACSHA256())
                        {
                            menuMod.PasswordSalt = hmac.Key;
                            menuMod.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(inputDto.NewPassword));
                        }
                        break;
                    default:
                        return Output<BaseOutputDto>(Constants.ResultCdFail, "There are some errors happened in the system. Debug for more info.");
                }
                context.SaveChanges();
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);

            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }

        }

        public GetOrderInfoOutputDto GetUserInfo(GetOrderInfoInputDto inputDto)
        {
            try
            {
                var user = this.context.Customers.Include(x => x.ShipAddresses).FirstOrDefault(x => x.CustomerId == inputDto.UserId);
                var output = this.Output<GetOrderInfoOutputDto>(Constants.ResultCdSuccess);
                if (user != null)
                {
                    output.Balance = user.WalletBalance == null ? 0 : user.WalletBalance.Value;
                    output.Address = user.ShipAddresses.FirstOrDefault() == null ? "" : user.ShipAddresses.FirstOrDefault().AddressInfo;
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetOrderInfoOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetUserAddressDaoOutputDto GetCusAddress(GetAddressInfoInputDto inputDto)
        {
            try
            {
                var user = this.context.ShipAddresses
                    .Where(x => x.CustomerId == inputDto.UserId)
                    .Select(x => new UserAddressDaoOutputDto
                    {
                        AddressId = x.AddressId,
                        AddressInfo = x.AddressInfo,
                        IsDefaultAddress = x.IsDefaultAddress,
                    });

                var output = this.Output<GetUserAddressDaoOutputDto>(Constants.ResultCdSuccess);
                output.ListAddress = user.ToList();

                return output;
            }
            catch (Exception)
            {
                return this.Output<GetUserAddressDaoOutputDto>(Constants.ResultCdFail);
            }
        }

        public UserRefreshToken? GetUserRefreshToken(string email, string role)
        {
            try
            {
                switch (role)
                {
                    case "CU":
                        return context.Customers.Select(x => new UserRefreshToken{
                            Id = x.CustomerId,
                            RefreshToken = x.RefreshToken,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
                        })
                        .SingleOrDefault(cu => cu.Email.Equals(email));
                    case "SE":
                        return context.Sellers.Select(x => new UserRefreshToken
                        {
                            Id = x.SellerId,
                            RefreshToken = x.RefreshToken,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
                        }).SingleOrDefault(se => se.Email.Equals(email));
                    case "SH":
                        return context.Shippers.Select(x => new UserRefreshToken
                        {
                            Id = x.ShipperId,
                            RefreshToken = x.RefreshToken,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
                        }).SingleOrDefault(sh => sh.Email.Equals(email));
                    case "AD":
                        return context.Admins.Select(x => new UserRefreshToken
                        {
                            Id = x.AdminId,
                            RefreshToken = x.RefreshToken,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
                        }).SingleOrDefault(ad => ad.Email.Equals(email));
                    case "PM":
                        return context.PostModerators.Select(x => new UserRefreshToken
                        {
                            Id = x.ModId,
                            RefreshToken = x.RefreshToken,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
                        }).SingleOrDefault(pm => pm.Email.Equals(email));
                    case "MM":
                        return context.MenuModerators.Select(x => new UserRefreshToken
                        {
                            Id = x.ModId,
                            RefreshToken = x.RefreshToken,
                            Email = x.Email,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            RefreshTokenExpiryTime = x.RefreshTokenExpiryTime
                        }).SingleOrDefault(mm => mm.Email.Equals(email));
                    default:
                        return null;
                   
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BaseOutputDto EditRefreshToken(UpdateRefreshToken inputDto)
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

                        if (customer == null) return Output<BaseOutputDto>(Constants.ResultCdFail);

                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        customer.RefreshToken = inputDto.RefreshToken;

                        break;
                    case "SE":
                        //Tim trong context profile cua user theo id
                        var seller = context.Sellers.SingleOrDefault(
                            se => se.SellerId.Equals(inputDto.UserId));

                        if (seller == null) return Output<BaseOutputDto>(Constants.ResultCdFail);

                        seller.RefreshToken = inputDto.RefreshToken;
                        break;
                    case "SH":
                        //Tim trong context profile cua user theo id
                        var shipper = context.Shippers.SingleOrDefault(
                            sh => sh.ShipperId.Equals(inputDto.UserId));

                        if (shipper == null) return Output<BaseOutputDto>(Constants.ResultCdFail);

                        shipper.RefreshToken = inputDto.RefreshToken;

                        break;
                    case "AD":
                        //Tim trong context profile cua user theo id
                        var admin = context.Admins.SingleOrDefault(
                            ad => ad.AdminId.Equals(inputDto.UserId));

                        if (admin == null) return Output<BaseOutputDto>(Constants.ResultCdFail);

                        admin.RefreshToken = inputDto.RefreshToken;

                        break;
                    case "PM":
                        //Tim trong context profile cua user theo id
                        var postModerator = context.PostModerators.SingleOrDefault(
                            pm => pm.ModId.Equals(inputDto.UserId));

                        if (postModerator == null) return Output<BaseOutputDto>(Constants.ResultCdFail);

                        postModerator.RefreshToken = inputDto.RefreshToken;

                        break;
                    case "MM":
                        //Tim trong context profile cua user theo id
                        var menuModerator = context.MenuModerators.SingleOrDefault(
                            mm => mm.ModId.Equals(inputDto.UserId));

                        if (menuModerator == null) return Output<BaseOutputDto>(Constants.ResultCdFail);

                        menuModerator.RefreshToken = inputDto.RefreshToken;
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

        public BaseOutputDto RevokeToken(RevokeToken inputDto)
        {
            try
            {
                switch (inputDto.Role)
                {
                    case "CU":
                        //Tim trong context profile cua user theo id
                        var customer = context.Customers.SingleOrDefault(
                            cu => cu.Email.Equals(inputDto.Email));
                        if (customer == null) return Output<BaseOutputDto>(Constants.ResultCdFail);
                        //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                        customer.RefreshToken = null;

                        break;
                    case "SE":
                        //Tim trong context profile cua user theo id
                        var seller = context.Sellers.SingleOrDefault(
                            se => se.Email.Equals(inputDto.Email));
                        if (seller == null) return Output<BaseOutputDto>(Constants.ResultCdFail);
                        seller.RefreshToken = null;
                        break;
                    case "SH":
                        //Tim trong context profile cua user theo id
                        var shipper = context.Shippers.SingleOrDefault(
                            sh => sh.Email.Equals(inputDto.Email));
                        if (shipper == null) return Output<BaseOutputDto>(Constants.ResultCdFail);
                        shipper.RefreshToken = null;

                        break;
                    case "AD":
                        //Tim trong context profile cua user theo id
                        var admin = context.Admins.SingleOrDefault(
                            ad => ad.Email.Equals(inputDto.Email));
                        if (admin == null) return Output<BaseOutputDto>(Constants.ResultCdFail);
                        admin.RefreshToken = null;

                        break;
                    case "PM":
                        //Tim trong context profile cua user theo id
                        var postModerator = context.PostModerators.SingleOrDefault(
                            pm => pm.Email.Equals(inputDto.Email));
                        if (postModerator == null) return Output<BaseOutputDto>(Constants.ResultCdFail);
                        postModerator.RefreshToken = null;

                        break;
                    case "MM":
                        //Tim trong context profile cua user theo id
                        var menuModerator = context.MenuModerators.SingleOrDefault(
                            mm => mm.Email.Equals(inputDto.Email));
                        if (menuModerator == null) return Output<BaseOutputDto>(Constants.ResultCdFail);
                        menuModerator.RefreshToken = null;
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
    }
}

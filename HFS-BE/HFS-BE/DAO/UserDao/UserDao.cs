using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.UserDao
{
    public class UserDao : BaseDao
    {
        public UserDao(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        /// <summary>
        /// Get the user's profile by user's id
        /// </summary>
        /// <param name="userId">The id of the user when they login</param>
        /// <returns>UserProfileOutputDto, which is the response of this function to the user 
        /// that consists of the user's profile, messages and success status </returns>
        public UserProfileOutputDto GetUserProfileById(int userId)
        {
            try
            {
                var user = context.Users.SingleOrDefault(up => up.UserId == userId);
                if (user == null)
                    return Output<UserProfileOutputDto>(Constants.ResultCdFail, "User not found. Please login first.");
                var datamapper = mapper.Map<User, UserProfile>(user);
                var output = Output<UserProfileOutputDto>(Constants.ResultCdSuccess);
                output.data = datamapper;
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
            try
            {
                //Tim trong context profile cua user theo id
                var profile = context.Users.SingleOrDefault(
                        p => p.UserId == inputDto.UserId
                    );
                //Check user co ton tai hay khong
                if (profile == null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"User with id: {inputDto.UserId} is not exist!");

                //Truong hop profile nguoi dung co ton tai thi cap nhat lai cac truong thong tin
                profile.FirstName = inputDto.FirstName;
                profile.LastName = inputDto.LastName;
                profile.Gender = inputDto.Gender;
                profile.BirthDate = inputDto.BirthDate;

                //Luu thay doi trong context vao db
                context.SaveChanges();

                //Output ra response body trang thai thanh cong
                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                //Output ra response body trang thai that bai neu co loi bat ky
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetOrderInfoOutputDto GetUserInfo(GetOrderInfoInputDto inputDto)
        {
            try
            {
                var user = this.context.Users.Include(x => x.ShipAddresses).FirstOrDefault(x => x.UserId == inputDto.UserId);
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
    }
}

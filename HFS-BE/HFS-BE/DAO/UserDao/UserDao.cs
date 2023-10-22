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
        /// <param name="inputDto">The input of the user</param>
        /// <returns>UserProfileOutputDto, which is used for displaying on the user's screen</returns>
        public UserProfileOutputDao GetUserProfileById(GetUserProfileInputDto inputDto)
        {
            try
            {
                var data = context.Users.SingleOrDefault(up => up.UserId == inputDto.UserId);
                var datmapper = mapper.Map<User, UserProfileOutputDto>(data);

                var output = Output<UserProfileOutputDao>(Constants.ResultCdSuccess);
                output.data = datmapper;
                return output;
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDao>(Constants.ResultCdFail);
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

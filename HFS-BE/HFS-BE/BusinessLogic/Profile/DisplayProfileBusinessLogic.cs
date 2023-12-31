﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Profile
{
    public class DisplayProfileBusinessLogic : BaseBusinessLogic
    {
        public DisplayProfileBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public UserProfileOutputDto GetProfile(string userId) {
            try
            {
                var dao = CreateDao<UserDao>();
                return dao.GetUserProfileById(userId);
            }
            catch (Exception)
            {
                return Output<UserProfileOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class BanUnbanPostBusinessLogic : BaseBusinessLogic
    {
        public BanUnbanPostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto BanUnbanPost(PostBanUnbanInputDto inputDto, string userId) {
            try
            {
                var postDao = CreateDao<PostDao>();
                return postDao.BanUnbanPost(inputDto, userId);
            }
            catch (Exception)
            {
                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

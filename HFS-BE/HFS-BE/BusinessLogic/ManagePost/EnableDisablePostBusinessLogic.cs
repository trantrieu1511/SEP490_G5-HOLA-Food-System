using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.SellerDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class EnableDisablePostBusinessLogic : BaseBusinessLogic
    {
        public EnableDisablePostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EnableDisablePost(PostEnableDisableInputDto input)
        {
            try
            {
                var sellerDao = CreateDao<SellerDao>();

                if (sellerDao.GetSellerByEmail(input.UserDto.Email) is null)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Your acccount is not exist");

                if (sellerDao.GetSellerByEmail(input.UserDto.Email).IsVerified == false)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Your acccount is not verified");

                if (sellerDao.GetSellerByEmail(input.UserDto.Email).IsBanned == true)
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Add Failed", "Your acccount is banned");

                var dao = this.CreateDao<PostDao>();
                var post = dao.GetPostById(input.PostId);
                var errors = new List<string>();
                if (post == null)
                {
                    errors.Add($"PostId: {input.PostId} not exist!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    //return Output<BaseOutputDto>(Constants.ResultCdFail, $"PostId: {input.PostId} not exist!");
                }
                // check status Ban 
                if (post.Status == 3)
                {
                    errors.Add($"PostId: {input.PostId} has been banned and cannot be changed!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    /*return Output<BaseOutputDto>(Constants.ResultCdFail,
                        $"PostId: {input.PostId} has been banned and cannot be changed!");*/
                }
                // check status Not Approved
                if (post.Status == 0)
                {
                    errors.Add($"PostId: {input.PostId} is pending acceptance and cannot be changed!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    /*return Output<BaseOutputDto>(Constants.ResultCdFail,
                        $"PostId: {input.PostId} is pending acceptance and cannot be changed!");*/
                }

                var output = dao.EnableDisablePost(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}

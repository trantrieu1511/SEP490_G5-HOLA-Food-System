using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class EnableDisablePostBusinessLogic : BaseBusinessLogic
    {
        public EnableDisablePostBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto EnableDisablePost(PostEnableDisableInputDto input)
        {
            try
            {
                var Dao = this.CreateDao<PostDao>();
                var output = Dao.EnableDisablePost(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}

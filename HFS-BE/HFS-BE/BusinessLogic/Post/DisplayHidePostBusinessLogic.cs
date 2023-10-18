using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Post
{
    public class DisplayHidePostBusinessLogic : BaseBusinessLogic
    {
        public DisplayHidePostBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto DisplayHidePost(PostDisplayHideInputDto input)
        {
            try
            {
                var Dao = this.CreateDao<PostDao>();
                var output = Dao.DisplayHidePost(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }
    }
}

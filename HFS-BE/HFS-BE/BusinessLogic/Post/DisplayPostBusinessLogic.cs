using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Ultis;

namespace HFS_BE.BusinessLogic.Post
{
    public class DisplayPostBusinessLogic : BaseBusinessLogic
    {
        public DisplayPostBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListPostOutputDto ListPosts() {
            try
            {
                var Dao = this.CreateDao<PostDao>();
                Dao.PostDao.ListPostOutputDto daoOutput = Dao.AllPosts();
                /*var output = mapper.Map<Dao.PostDao.ListPostOutputDto, ListPostOutputDto>(daoOutput);
                return output;*/
                return daoOutput;
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

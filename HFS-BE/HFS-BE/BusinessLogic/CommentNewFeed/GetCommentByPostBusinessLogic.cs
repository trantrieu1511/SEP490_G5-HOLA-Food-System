using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.CommentNewFeed
{
    public class GetCommentByPostBusinessLogic : BaseBusinessLogic
    {
        public GetCommentByPostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public GetCommentByPostOutputDto GetComment(GetCommentInputDto inputDto)
        {
            try
            {
                var dao = this.CreateDao<CommentNewFeedDao>();
                var output = dao.GetCommentByPost(inputDto);

                return output;

            }
            catch (Exception)
            {

                return this.Output<GetCommentByPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

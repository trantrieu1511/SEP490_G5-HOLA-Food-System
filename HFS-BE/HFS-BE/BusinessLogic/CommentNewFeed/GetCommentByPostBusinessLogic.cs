using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CommentNewFeedDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;
using Mailjet.Client.Resources;

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
				
                foreach (var item in output.ListComment)
                {
					ImageFileConvert.ImageOutputDto? imageInfor = null;
					if (item.Avartar == null)
					{
                        break;

					}
					imageInfor = ImageFileConvert.ConvertFileToBase64(item.CustomerId, item.Avartar, 3);
                    item.Avartar = imageInfor.ImageBase64;

				}
			
			
				return output;

            }
            catch (Exception)
            {

                return this.Output<GetCommentByPostOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

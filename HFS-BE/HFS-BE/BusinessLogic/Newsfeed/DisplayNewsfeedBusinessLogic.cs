using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.ManagePost;
using HFS_BE.DAO.NewsfeedDao;
using HFS_BE.DAO.UserDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.Newsfeed
{
    public class DisplayNewsfeedBusinessLogic : BaseBusinessLogic
    {
        public DisplayNewsfeedBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListNewsfeedOutputDto GetAllNews()
        {
            try
            {
                var dao = CreateDao<NewsfeedDao>();
                DAO.NewsfeedDao.ListNewsfeedOutputDto daoOutput = dao.GetAllNews();

                var output = mapper.Map<DAO.NewsfeedDao.ListNewsfeedOutputDto, ListNewsfeedOutputDto>(daoOutput);

                foreach (var post in daoOutput.News)
                {
                    if (post.Images == null || post.Images.Count < 1)
                        continue;
                    // get current index
                    var index = daoOutput.News.IndexOf(post);
                    foreach (var img in post.Images)
                    {
                        ImageFileConvert.ImageOutputDto? imageInfor = null;
                        // convert to base64
                        imageInfor = ImageFileConvert.ConvertFileToBase64(post.SellerId, img.Path, 0);

                        if (imageInfor == null)
                            continue;

                        var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, PostImageOutputDto>(imageInfor);
                        imageMapper.ImageId = img.ImageId;

                        // add to ouput list
                        output.News[index].ImagesBase64.Add(imageMapper);
                    }
                }

                return output;
            }
            catch (Exception e)
            {
                return Output<ListNewsfeedOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }
    }
}

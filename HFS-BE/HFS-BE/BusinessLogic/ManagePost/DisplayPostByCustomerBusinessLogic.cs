using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class DisplayPostByCustomerBusinessLogic : BaseBusinessLogic
    {
        public DisplayPostByCustomerBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public ListPostOutputCustomerDto ListPostsByCustomer(PostStatusInputDto input)
        {
            try
            {


                var Dao = this.CreateDao<PostDao>();
                var dao = Dao.ListPostsByCustomer(input);
                var output = mapper.Map<Dao.PostDao.ListPostByCustomerOutputDto, ListPostOutputCustomerDto>(dao);

                foreach (var post in dao.Posts)
                {
                    if (post.PostImages == null || post.PostImages.Count < 1)
                        continue;
                    // get current index
                    var index = dao.Posts.IndexOf(post);
                    foreach (var img in post.PostImages)
                    {
                        ImageFileConvert.ImageOutputDto? imageInfor = null;
                        // convert to base64
                        // post moderator case

                        imageInfor = ImageFileConvert.ConvertFileToBase64(post.SellerId, img.Path, 0);

                        if (imageInfor == null)
                            continue;

                        var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, PostImageOutputCustomerDto>(imageInfor);
                        imageMapper.ImageId = img.ImageId;

                        // add to ouput list
                        output.Posts[index].ImagesBase64.Add(imageMapper);
                    }
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputCustomerDto>(Constants.ResultCdFail);
            }
        }
    }
}

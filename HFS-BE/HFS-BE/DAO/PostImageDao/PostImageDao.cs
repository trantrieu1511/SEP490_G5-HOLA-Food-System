using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.PostImageDao
{
    public class PostImageDao : BaseDao
    {
        public PostImageDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public List<PostImage>? GetAllImageByPostId(int postId)
        {
            var output = context.PostImages.Where(
                    i => i.PostId == postId
                ).ToList();
            if (output.Count > 0)
                return output;
            return null;
        }

        public BaseOutputDto UpdateImageInfor(PostImage image)
        {
            try
            {
                var imageModel = context.PostImages.FirstOrDefault(
                    img => img.ImageId == image.ImageId
                );

                imageModel.Path = image.Path;
                imageModel.PostId = image.PostId;

                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        /// <summary>
        /// Create new image of post.
        /// </summary>
        /// <param name="image"></param>
        /// <returns>BaseOutput</returns>
        public BaseOutputDto AddNewImage(PostImage image)
        {
            try
            {
                context.Add(image);
                context.SaveChanges();

                return Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

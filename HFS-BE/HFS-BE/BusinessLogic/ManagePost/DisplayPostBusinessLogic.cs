using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class DisplayPostBusinessLogic : BaseBusinessLogic
    {
        public DisplayPostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListPostOutputSellerDto ListPosts(UserDto userDto)
        {
            try
            {
                /*userDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 1,
                    UserId = 1,
                };*/

                var Dao = this.CreateDao<PostDao>();


                // Seller
                Dao.PostDao.ListPostOutputSellerDto daoOutput = Dao.GetAllPostSeller(userDto);

                // post moderator thì gọi method dao khác select thêm shopID nx 
                // output thêm trường shopId r -> xài dùng output ListPostOutputSellerDto
                // thiêm truòng j trả về thì thềm vào ListPostOutputSellerDto ở BL, Dao - OK

                // Post moderator
                if (userDto.UserId.Substring(0, 2).Equals("PM"))
                {
                    daoOutput = Dao.GetAllPostPostModerator();
                }
                var output = mapper.Map<Dao.PostDao.ListPostOutputSellerDto, ListPostOutputSellerDto>(daoOutput);

                foreach (var post in daoOutput.Posts)
                {
                    if (post.Images == null || post.Images.Count < 1)
                        continue;
                    // get current index
                    var index = daoOutput.Posts.IndexOf(post);
                    foreach (var img in post.Images)
                    {
                        ImageFileConvert.ImageOutputDto? imageInfor = null;
                        // convert to base64
                        // post moderator case
                        if (userDto.UserId.Substring(0, 2).Equals("PM"))
                        {
                            imageInfor = ImageFileConvert.ConvertFileToBase64(post.SellerId, img.Path, 0);
                        }
                        else // seller case
                        {
                            imageInfor = ImageFileConvert.ConvertFileToBase64(userDto.UserId, img.Path, 0);
                        }
                        if (imageInfor == null)
                            continue;

                        var imageMapper = mapper.Map<ImageFileConvert.ImageOutputDto, PostImageOutputSellerDto>(imageInfor);
                        imageMapper.ImageId = img.ImageId;

                        // add to ouput list
                        output.Posts[index].ImagesBase64.Add(imageMapper);
                    }
                }

                return output;
            }
            catch (Exception)
            {
                return this.Output<ListPostOutputSellerDto>(Constants.ResultCdFail);
            }
        }

    }
}

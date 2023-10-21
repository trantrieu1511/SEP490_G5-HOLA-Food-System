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
        public DisplayPostBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListPostOutputSellerDto ListPosts(UserDto userDto)
        {
            try
            {
                userDto = new UserDto
                {
                    Email = "test@gmail.com",
                    Name = "testSeller",
                    RoleId = 1,
                    UserId = 1,
                };

                var Dao = this.CreateDao<PostDao>();
                Dao.PostDao.ListPostOutputSellerDto daoOutput = Dao.GetAllPostSeller(userDto);
                var output = mapper.Map<Dao.PostDao.ListPostOutputSellerDto, ListPostOutputSellerDto>(daoOutput);
                // post moderator thì gọi method dao khác select thêm shopID nx 
                // output thêm trường shopId r -> xài dùng output ListPostOutputSellerDto
                // thiêm truòng j trả về thì thềm vào ListPostOutputSellerDto ở BL, Dao
                foreach (var post in daoOutput.Posts)
                {
                    if(post.Images == null ||post.Images.Count < 1) 
                        continue;
                    // get current index
                    var index = daoOutput.Posts.IndexOf(post);
                    foreach (var img in post.Images)
                    {
                        // convert to base64
                        var imageInfor = ImageFileConvert.ConvertFileToBase64(userDto, img.Path, 0);
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

namespace HFS_BE.Controllers.ManagePost
{
    public class PostUpdateInputDto
    {
        public int PostId { get; set; }
        public string? PostContent { get; set; }
        public List<IFormFile>? Images { get; set; }
    }

    public class ImageInput
    {
        public string ObjectURL { get; set; }
    }

   

    public class PostImageBase64
    {
        public int ImageId { get; set; }
        public string? ImageBase64 { get; set; }
        public string? Name { get; set; }
        public string? Size { get; set; }
    }
}

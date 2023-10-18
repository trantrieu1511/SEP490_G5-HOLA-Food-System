namespace HFS_BE.Utils.IOFile
{
    public static class ImageFileConvert
    {
        public class ImageOutputDto
        {
            public string? ImageBase64 { get; set; }
            public string? Name { get; set; }
            public string? Size { get; set; }
        }

        public static ImageOutputDto? ConvertFileToBase64(UserDto user,string fileName, int type)
        {
            string path = $"Resources\\Images\\" +
                            $"{user.UserId}\\" +
                            $"{GetFolderNameTypeImage(type)}\\"
                            + fileName;
            // Đường dẫn cơ sở cho việc lưu hình ảnh
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), path);

            if (File.Exists(fullPath))
            {
                try
                {
                    // Read the image file into a byte array
                    byte[] imageBytes = File.ReadAllBytes(path);

                    // Convert the byte array to a base64 string
                    string base64String = Convert.ToBase64String(imageBytes);

                    // Get the file size in bytes and convert to KB string
                    var fileSize = (new FileInfo(path).Length / 1024.0);
                    string sizeRound = Math.Round(fileSize, 3).ToString();

                    return new ImageOutputDto { ImageBase64 = base64String, Name = fileName, Size = sizeRound };
                }
                catch (Exception ex)
                {
                    return null;
                   // return "Error: " + ex.Message;
                }
            }
            else
            {
                return null;
                //return "The specified file does not exist.";
            }
        }

        private static string GetFolderNameTypeImage(int type)
        {
            switch (type)
            {
                case 0:
                    return "post";
                case 1:
                    return "food";
                default:
                    return "";
            }
        }
    }
}

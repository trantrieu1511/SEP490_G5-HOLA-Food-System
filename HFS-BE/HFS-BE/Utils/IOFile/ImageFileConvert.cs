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

        /// <summary>
        /// Convert the image file into base64 string.
        /// </summary>
        /// <param name="userId">The id of the user, used in the full path of the stored image</param>
        /// <param name="fileName">Name of the image file that is saved to the server database</param>
        /// <param name="type">The image category of the image (food, post, order progress) </param>
        /// <returns>ImageOutputDto, which is the output object of the converted to base64 image which 
        /// consist of attributes: ImageBase64, name and size</returns>
        public static ImageOutputDto? ConvertFileToBase64(string userId, string fileName, int type)
        {
            if (fileName == null || fileName.Equals(""))
                return null;

            string path = $"Resources\\Images\\" +
                            $"{userId}\\" +
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
                case 2:
                    return "profile";
				case 4:
					return "feedback";
				default:
                    return "";
            }
        }
    }
}

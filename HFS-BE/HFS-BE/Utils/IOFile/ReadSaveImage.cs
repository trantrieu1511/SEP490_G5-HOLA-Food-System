using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace HFS_BE.Utils.IOFile
{
    public static class ReadSaveImage
    {

        /// <summary>
        /// Save the images inputted by users in the API to the server's local path
        /// </summary>
        /// <param name="images">Images inputted by users, has type of IReadOnlyList<IFormFile></param>
        /// <param name="user">The basic info of users which include userId, name, email, role</param>
        /// <param name="type">The category which the image belongs to (post images, food images, 
        /// order progress images,...)</param>
        /// <returns>List of fileName in order to save to db</returns>
        public static List<string> SaveImages(IReadOnlyList<IFormFile> images, UserDto user, int type)
        {
            string basePath = $"Resources/Images/" +
                            $"{user.UserId}/" +
                            $"{GetFolderNameTypeImage(type)}";
            // Đường dẫn cơ sở cho việc lưu hình ảnh
            //string fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath);
            Console.WriteLine("fullpath1" + Path.Combine(Directory.GetCurrentDirectory(), basePath));
            // product enviroment
            string fullPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), basePath);
            Console.WriteLine("fullpath2" + fullPath);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            var output = new List<string>();
            foreach (var file in images)
            {
                if (file.Length > 0)
                {
                    string insertTime = $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "")
                        + insertTime
                        + Path.GetExtension(file.FileName);

                    string filePath = Path.Combine(fullPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    output.Add(fileName);
                }
            }
            return output;
        }

        public static string SaveImagesOrderProgress(IFormFile image, UserDto user, int type)
        {
            string basePath = $"Resources/Images/" +
                            $"{user.UserId}/" +
                            $"{GetFolderNameTypeImage(type)}";
            // Đường dẫn cơ sở cho việc lưu hình ảnh
           // string fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath);
string fullPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), basePath);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            string output = "";

            if (image.Length > 0)
            {
                string insertTime = $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
                string fileName = Path.GetFileNameWithoutExtension(image.FileName).Replace(" ", "")
                    + insertTime
                    + Path.GetExtension(image.FileName);

                string filePath = Path.Combine(fullPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                output = fileName;


            }

            return output;
        }

        /// <summary>
        /// Save the profile image inputted by users in the API to the server's local path and return the formatted filename
        /// </summary>
        /// <param name="images">Images inputted by users, has type of IReadOnlyList<IFormFile></param>
        /// <param name="user">The basic info of users which include userId, name, email, role</param>
        /// <returns>The file's name, in order to save to db</returns>
        public static string SaveProfileImage(IFormFile image, string userId)
        {
             string basePath = $"Resources/Images/" +
                            $"{userId}/" +
                            $"{GetFolderNameTypeImage(3)}";
            // Đường dẫn cơ sở cho việc lưu hình ảnh
        //    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath);
           // product enviroment
            string fullPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), basePath);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            string insertTime = $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
            string fileName = Path.GetFileNameWithoutExtension(image.FileName).Replace(" ", "")
                + insertTime
                + Path.GetExtension(image.FileName);

            string filePath = Path.Combine(fullPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            string? output = fileName;

            return output;
        }
		public static List<string> SaveSellerReportImages(IReadOnlyList<IFormFile> images, string userId, int type)
		{
			 string basePath = $"Resources/Images/" +
                            $"{userId}/" +
                            $"{GetFolderNameTypeImage(type)}";
			// Đường dẫn cơ sở cho việc lưu hình ảnh
		//	string fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath);
                       // product enviroment
                    string fullPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), basePath);
			// Tạo thư mục nếu chưa tồn tại
			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
			}

			var output = new List<string>();
			foreach (var file in images)
			{
				if (file.Length > 0)
				{
					string insertTime = $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
					string fileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "")
						+ insertTime
						+ Path.GetExtension(file.FileName);

					string filePath = Path.Combine(fullPath, fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

					output.Add(fileName);
				}
			}
			return output;
		}
		public static string SaveIdCardImages(IFormFile? image, string userId, int type)
		{
			string basePath = $"Resources\\Images\\" +
							$"{userId}\\" +
							$"{GetFolderNameTypeImage(type)}";
			// Đường dẫn cơ sở cho việc lưu hình ảnh
			string fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath);

			// Tạo thư mục nếu chưa tồn tại
			if (!Directory.Exists(fullPath))
			{
				Directory.CreateDirectory(fullPath);
			}

			string insertTime = $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
			string fileName = Path.GetFileNameWithoutExtension(image.FileName).Replace(" ", "")
				+ insertTime
				+ Path.GetExtension(image.FileName);

			string filePath = Path.Combine(fullPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				image.CopyTo(stream);
			}

			string? output = fileName;

			return output;
		}
		public static List<string> SaveLicenseImages(IReadOnlyList<IFormFile> images, UserDto user, int type)
        {
            string basePath = $"Resources/Images/" +
                            $"{user.UserId}/" +
                            $"{GetFolderNameTypeImage(type)}";
            // Đường dẫn cơ sở cho việc lưu hình ảnh
       //     string fullPath = Path.Combine(Directory.GetCurrentDirectory(), basePath);
               string fullPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), basePath);
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            var output = new List<string>();
            foreach (var file in images)
            {
                if (file.Length > 0)
                {
                    string insertTime = $"_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
                    string fileName = Path.GetFileNameWithoutExtension(file.FileName).Replace(" ", "")
                        + insertTime
                        + Path.GetExtension(file.FileName);

                    string filePath = Path.Combine(fullPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    output.Add(fileName);
                }
            }
            return output;
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
                    return "orderprogress";
                case 3:
                    return "profile";
				case 4:
					return "feedback";
				case 5:
					return "reportseller";
				case 6:
					return "license";
				case 7:
					return "idcard";
				default:
                    return "";
            }
        }

    }
}

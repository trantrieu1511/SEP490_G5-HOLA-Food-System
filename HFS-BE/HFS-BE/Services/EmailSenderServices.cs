using System.Text;
using System.Web;

namespace HFS_BE.Services
{
	public class EmailSenderServices
	{
		private const string MailgunApiBaseUrl = "https://api.mailgun.net/v3/";
		private const string MailgunDomain = "YOUR_MAILGUN_DOMAIN";
		private const string MailgunApiKey = "YOUR_MAILGUN_API_KEY";

		public async Task SendConfirmationEmail(string toEmail, string confirmLink)
		{
			string subject = "Xác nhận thay đổi trạng thái";
			string message = $"Vui lòng nhấp vào liên kết sau để xác nhận thay đổi trạng thái: {confirmLink}";

			var httpClient = new HttpClient();
			httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
				Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{MailgunApiKey}")));

			var content = new FormUrlEncodedContent(new[]
			{
			new KeyValuePair<string, string>("from", "YOUR_SENDER_EMAIL"),
			new KeyValuePair<string, string>("to", toEmail),
			new KeyValuePair<string, string>("subject", subject),
			new KeyValuePair<string, string>("text", message)
		});

			var response = await httpClient.PostAsync($"{MailgunApiBaseUrl}{MailgunDomain}/messages", content);

			// Xử lý kết quả
			if (response.IsSuccessStatusCode)
			{
				// Gửi email thành công
			}
			else
			{
				// Xảy ra lỗi khi gửi email
			}
		}

		public string GenerateConfirmationLink(string userId, string confirmationCode)
		{
			// URL cần tạo liên kết xác nhận
			string baseUrl = "https://example.com/confirm";

			// Tạo liên kết với các tham số cần thiết
			string confirmationLink = $"{baseUrl}?userId={HttpUtility.UrlEncode(userId)}&code={HttpUtility.UrlEncode(confirmationCode)}";

			return confirmationLink;
		}

	}

}

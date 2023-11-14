using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Auth;
using HFS_BE.Dao.AuthDao;
using HFS_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace HFS_BE.Controllers.Auth
{
	
	public class ForgotPasswordController : BaseController
	{
		public ForgotPasswordController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
		{
		}
		[HttpPost("home/sendforgot")]
		public async Task<BaseOutputDto> Forgot(ForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ForgotPasswordBusinessLogic>();

				return await business.SendForgotPassword(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/confirmforgot")]
		public BaseOutputDto ConfirmForgot(ConfirmForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ForgotPasswordBusinessLogic>();

				return  business.ConfirmForgotPassword(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("home/changepassword")]
		public BaseOutputDto ChangeForgot(ChangeForgotPasswordInputDto inputDto)
		{
			try
			{
				var business = this.GetBusinessLogic<ForgotPasswordBusinessLogic>();

				return business.ChangeForgotPassword(inputDto);
			}
			catch (Exception ex)
			{
				throw;
			}
		}
		[HttpPost("okdi")]
		public async Task<IActionResult> SendMail(string toEmail, string content)
		{
			try
			{
				string from = "holafoodfpt@gmail.com";
				string pass = "wqsq fqmv iwhu ablr";
				MailMessage mail = new MailMessage();
				SmtpClient smtp = new SmtpClient("smtp.gmail.com");
				Random r = new Random();
				int random = r.Next(1000, 9999);
				mail.To.Add(toEmail);
				mail.From = new MailAddress(from);
				mail.Subject = "PRN221";
				mail.Body = "Code:" + random;
				smtp.EnableSsl = true;
				smtp.Port = 587;
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Credentials = new NetworkCredential(from, pass);
				await smtp.SendMailAsync(mail);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest();
			}

		}
	}
}

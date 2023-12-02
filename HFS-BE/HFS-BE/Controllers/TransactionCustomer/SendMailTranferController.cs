using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using HFS_BE.Utils;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace HFS_BE.Controllers.TransactionCustomer
{
    public class SendMailTranferController : BaseController
    {
        public SendMailTranferController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("wallet/sendmail")]
        [Authorize]
        public BaseOutputDto SendEmailCode()
        {
            try
            {
                var userInfor = this.GetUserInfor();
                var code = this.GenerateCode(6);


                var output = this.SendEmail2Async(userInfor.Email, "Hola Food Transfer Code", "This is your transfer code: " + code);
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        private async Task<bool> SendEmail2Async(string toEmail, string subject, string content)
        {
            try
            {
                string from = "holafoodfpt@gmail.com";
                string pass = "wqsq fqmv iwhu ablr";
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                mail.To.Add(toEmail);
                mail.From = new MailAddress(from);
                mail.Subject = subject;
                mail.Body = "HOLA FOOD:" + content;
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential(from, pass);
                await smtp.SendMailAsync(mail);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private string GenerateCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder code = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                code.Append(chars[index]);
            }

            return code.ToString();
        }
    }
}

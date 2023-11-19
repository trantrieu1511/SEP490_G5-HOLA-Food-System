using AutoMapper;
using CloudinaryDotNet.Actions;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace HFS_BE.Controllers.Payment
{
    public class VNPayPaymentController : BaseController
    {
        public VNPayPaymentController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("payment/getpaymenturl")]
        //[Authorize]
        public IActionResult Payment(CreateTransaction inputDto)
        {
            try 
            {
                //var userInfo = this.GetUserInfor();
                string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
                string vnp_Api = "https://sandbox.vnpayment.vn/merchant_webapi/api/transaction";
                string vnp_TmnCode = "UL1C3DJ9";
                string vnp_HashSecret = "WEEDAMWSSYKXZVVRHGSSPRDZLICKFZMN";
                string vnp_Returnurl = "http://localhost:4200/#/paymentverify";
                string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

                // Create TransactionHistory



                //Build URL for VNPAY
                VnPayLibrary vnpay = new VnPayLibrary();

                vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                vnpay.AddRequestData("vnp_Amount", (inputDto.Value * 100).ToString()); //Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 100,000 VND (một trăm nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 10000000

                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                vnpay.AddRequestData("vnp_IpAddr", ipAddress);
                vnpay.AddRequestData("vnp_Locale", "vn");

                //else if (locale_En.Checked == true)
                //{
                //    vnpay.AddRequestData("vnp_Locale", "en");
                //}
                //vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + userInfo.UserId + " " + DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan:" + "CU00000001" + " " + DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                //vnpay.AddRequestData("vnp_TxnRef", userInfo.UserId + " " + DateTime.Now.ToString("yyyyMMddHHmmss")); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
                vnpay.AddRequestData("vnp_TxnRef", "CU00000001" + " " + DateTime.Now.ToString("yyyyMMddHHmmss")); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                //Add Params of 2.1.0 Version
                //Billing

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

                return Ok(paymentUrl);
            }
            catch (Exception)
            {
                return Ok(this.Output<BaseOutputDto>(Constants.ResultCdFail));
            }
        }
    }
}

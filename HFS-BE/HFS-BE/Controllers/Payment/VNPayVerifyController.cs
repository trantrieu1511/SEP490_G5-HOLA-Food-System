using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace HFS_BE.Controllers.Payment
{
    public class VNPayVerifyController : BaseController
    {
        public VNPayVerifyController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("payment/verify")]
        public PaymentReturnOutputDto PaymentVerify(PaymentReturnInputDto inputDto)
        {
            try
            {
                string vnp_HashSecret = "WEEDAMWSSYKXZVVRHGSSPRDZLICKFZMN";
                // Lấy query string từ URL
                var queryString = inputDto.ReturnUrl.TrimStart('?');
                var output = new PaymentReturnOutputDto();

                // Parse query string thành một collection
                var vnpayData = HttpUtility.ParseQueryString(queryString);
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                string orderId = vnpay.GetResponseData("vnp_TxnRef");
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = vnpayData["vnp_SecureHash"];
                string TerminalID = vnpayData["vnp_TmnCode"];
                var a = vnpay.GetResponseData("vnp_Amount");
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                string bankCode = vnpayData["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        output.Success = true;
                        output.Message = "Success";
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        output.Success = false;
                        output.Message = "False";
                    }
                    output.VNPayTranID = vnpayTranId.ToString();
                    output.Value = Convert.ToDecimal(vnp_Amount.ToString());
                    output.BankCode = bankCode;
                }
                else
                {
                    output.Success = false;
                    output.Message = "False";
                }
                return output;
            }
            catch (Exception)
            {
                return this.Output<PaymentReturnOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

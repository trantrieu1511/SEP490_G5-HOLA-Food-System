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
                var output = new PaymentReturnOutputDto();
                VnPayLibrary vnpay = new VnPayLibrary();

                string orderId = inputDto.vnp_TxnRef;
                long vnpayTranId = Convert.ToInt64(inputDto.vnp_TransactionNo);
                string vnp_ResponseCode = inputDto.vnp_ResponseCode;
                string vnp_TransactionStatus = inputDto.vnp_TransactionStatus;
                string vnp_SecureHash = inputDto.vnp_SecureHash;
                string TerminalID = inputDto.vnp_TmnCode;
                long vnp_Amount = Convert.ToInt64(inputDto.vnp_Amount) / 100;
                string bankCode = inputDto.vnp_BankCode;

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

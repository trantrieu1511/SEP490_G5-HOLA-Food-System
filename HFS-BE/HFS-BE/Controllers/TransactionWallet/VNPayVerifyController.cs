﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.BusinessLogic.Transaction;
using HFS_BE.DAO.ChatMessageDao;
using HFS_BE.DAO.TransantionDao;
using HFS_BE.Models;
using HFS_BE.Services;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace HFS_BE.Controllers.TransactionWallet
{
    public class VNPayVerifyController : BaseController
    {
        public VNPayVerifyController(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        [HttpPost("payment/verify")]
        [Authorize]
        public PaymentReturnOutputDto PaymentVerify(PaymentReturnInputDto inputDto)
        {
            try
            {
                string vnp_HashSecret = "WEEDAMWSSYKXZVVRHGSSPRDZLICKFZMN";
                // Lấy query string từ URL
                var output = new PaymentReturnOutputDto();
                var userInfo = this.GetUserInfor();
                VnPayLibrary vnpay = new VnPayLibrary();


                string orderId = inputDto.vnp_TxnRef;
                long vnpayTranId = Convert.ToInt64(inputDto.vnp_TransactionNo);
                string vnp_ResponseCode = inputDto.vnp_ResponseCode;
                string vnp_TransactionStatus = inputDto.vnp_TransactionStatus;
                string vnp_SecureHash = inputDto.vnp_SecureHash;
                string TerminalID = inputDto.vnp_TmnCode;
                long vnp_Amount = Convert.ToInt64(inputDto.vnp_Amount) / 100;
                string bankCode = inputDto.vnp_BankCode;
                string vnp_TxnRef = inputDto.vnp_TxnRef;

                vnpay.AddResponseData("vnp_BankCode", inputDto.vnp_BankCode);
                vnpay.AddResponseData("vnp_Amount", inputDto.vnp_Amount);
                vnpay.AddResponseData("vnp_TmnCode", inputDto.vnp_TmnCode);
                vnpay.AddResponseData("vnp_SecureHash", inputDto.vnp_SecureHash);
                vnpay.AddResponseData("vnp_TransactionStatus", inputDto.vnp_TransactionStatus);
                vnpay.AddResponseData("vnp_ResponseCode", inputDto.vnp_ResponseCode);
                vnpay.AddResponseData("vnp_TransactionNo", inputDto.vnp_TransactionNo);
                vnpay.AddResponseData("vnp_TxnRef", inputDto.vnp_TxnRef);
                vnpay.AddResponseData("vnp_BankTranNo", inputDto.vnp_BankTranNo);
                vnpay.AddResponseData("vnp_CardType", inputDto.vnp_CardType);
                vnpay.AddResponseData("vnp_PayDate", inputDto.vnp_PayDate);
                vnpay.AddResponseData("vnp_OrderInfo", inputDto.vnp_OrderInfo);

                output.Value = vnp_Amount;
                output.VNPayTranID = vnpayTranId;

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        output.Success = true;
                        output.Message = "Success";
                        // update status transaction
                        var updateTransation = this.GetBusinessLogic<UpdateTransationStatusBusinessLogic>();
                        var transaction = new UpdateTransactionStatusInputDto()
                        {
                            UserId = userInfo.UserId,
                            Value = vnp_Amount,
                            TransactionId = Convert.ToInt32(vnp_TxnRef),
                            Status = 1,
                        };
                        var output1 = updateTransation.UpdateTransationStatus(transaction);
                        if (!output1.Success)
                        {
                            return this.Output<PaymentReturnOutputDto>(Constants.ResultCdFail);
                        }
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        output.Success = false;
                        output.Message = "False";
                        var updateTransation = this.GetBusinessLogic<UpdateTransationStatusBusinessLogic>();
                        var transaction = new UpdateTransactionStatusInputDto()
                        {
                            TransactionId = Convert.ToInt32(vnp_TxnRef),
                            Status = 2,
                        };
                        var output1 = updateTransation.UpdateTransationStatus(transaction);
                        if (!output1.Success)
                        {
                            return this.Output<PaymentReturnOutputDto>(Constants.ResultCdFail);
                        }
                    }
                    output.VNPayTranID = vnpayTranId;
                    output.Value = vnp_Amount;
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
using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CustomerDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Mailjet.Client.Resources;
using System.Text;

namespace HFS_BE.DAO.VoucherDao
{
    public class VoucherDao : BaseDao
    {
        public VoucherDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateVoucher(CreateVoucherDaoInputDto inputDto)
        {
            try
            {
                while (true)
                {
                    Voucher voucher = new Voucher
                    {
                        SellerId = inputDto.SellerId,
                        Code = GenerateVoucherCode(10),
                        DiscountAmount = inputDto.DiscountAmount,
                        MinimumOrderValue = inputDto.MinimumOrderValue,
                        Status = 0,
                        EffectiveDate = inputDto.EffectiveDate,
                        ExpireDate = inputDto.ExpireDate
                    };
                    var datacheck = context.Vouchers.Where(x => x.Code == voucher.Code).ToList();
                    if (datacheck.Count == 0)
                    {
                        context.Vouchers.Add(voucher);
                        context.SaveChanges();
                        return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                    }
                }
                
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public GetListVoucherDaoOutputDto GetListVoucher(GetListVoucherDaoInput inputDto)
        {
            try
            {
                var data = this.context.Vouchers.Where(x=>x.SellerId== inputDto.SellerId).ToList();
                
                var output = this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdSuccess);
                output.listVoucher = mapper.Map<List<Voucher>, List<GetVoucherDaoOutputDto>>(data);
                return output;
            }
            catch (Exception)
            {

                return this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }
        public GetListVoucherDaoOutputDto GetShopVoucher(GetListVoucherDaoInput inputDto)
        {
            try
            {
                var now = DateTime.Now;
                var data = this.context.Vouchers
                    .Where(x => x.SellerId.Equals(inputDto.SellerId)
                                && x.EffectiveDate <= now
                                && x.ExpireDate >= now
                                && x.Status == 1)
                    .ToList();

                var output = this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdSuccess);
                output.listVoucher = mapper.Map<List<Voucher>, List<GetVoucherDaoOutputDto>>(data);
                return output;
            }
            catch (Exception)
            {

                return this.Output<GetListVoucherDaoOutputDto>(Constants.ResultCdFail);
            }
        }


        public BaseOutputDto UpdateVoucher(UpdateVoucherDaoInput inputDto)
        {
            try
            {
                var data = this.context.Vouchers.FirstOrDefault(x=>x.VoucherId == inputDto.VoucherId);
                if(data == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"VoucherId: {inputDto.VoucherId} not exist!");
                }
                data.DiscountAmount = inputDto.DiscountAmount;
                data.EffectiveDate = inputDto.EffectiveDate;
                data.ExpireDate = inputDto.ExpireDate;
                context.SaveChanges();
                
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto Enable_Disable_Voucher(Enable_Disable_VoucherDaoInput inputDto)
        {
            try
            {
                var data = this.context.Vouchers.FirstOrDefault(x => x.VoucherId == inputDto.VoucherId);
                if (data == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"VoucherId: {inputDto.VoucherId} not exist!");
                }
                if (inputDto.Type)
                {
                    data.Status = 1;
                    context.SaveChanges();
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                data.Status = 2;
                context.SaveChanges();

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
        private string GenerateVoucherCode(int length)
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

        public Voucher? GetVoucherById(int voucherId)
        {
            return context.Vouchers.FirstOrDefault(x => x.VoucherId == voucherId);
        }

        public Voucher? GetVoucherByCode(string code)
        {
            return context.Vouchers.FirstOrDefault(x => x.Code.ToLower().Equals(code.ToLower()) && x.Status == 1);
        }

        public bool CheckUsedVoucher(string customerId, int voucherId)
        {
            var data = this.context.Orders.Where(x => x.CustomerId.Equals(customerId) && x.VoucherId == voucherId);
            return data.Any();
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Category
{
    public class EDCategoryBusinessLogic : BaseBusinessLogic
    {
        public EDCategoryBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto EnableDisableCate(Enable_Disable_CateInputDto input)
        {
            try
            {
                var dao = this.CreateDao<CategoryDao>();
                var cate = dao.GetCategoryById(input.CategoryId);
                var errors = new List<string>();
                if (cate == null)
                {
                    errors.Add($"CategoryId: {input.CategoryId} not exist!");
                    return Output<BaseOutputDto>(Constants.ResultCdFail, "Enable/Disable Failed", errors);
                    //return Output<BaseOutputDto>(Constants.ResultCdFail, $"PostId: {input.PostId} not exist!");
                }
                // check status Ban 
                var output = dao.Enable_Disable_Cate(input);

                return output;
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

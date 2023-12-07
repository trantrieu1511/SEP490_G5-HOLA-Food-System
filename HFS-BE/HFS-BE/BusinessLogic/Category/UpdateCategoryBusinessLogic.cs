using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Category
{
    public class UpdateCategoryBusinessLogic : BaseBusinessLogic
    {
        public UpdateCategoryBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto UpdateCategory(UpdateCategoryDaoInputDto inputDto)
        {
            try
            {
                var daoCate = CreateDao<CategoryDao>();
                var output = daoCate.UpdateCategory(inputDto);
                return output;
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

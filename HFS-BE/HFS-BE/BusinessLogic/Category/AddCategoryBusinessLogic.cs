using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Category
{
    public class AddCategoryBusinessLogic : BaseBusinessLogic
    {
        public AddCategoryBusinessLogic(SEP490_HFSContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto AddCategory(CategoryDaoInputDto inputDto)
        {
            try
            {
                var Dao = this.CreateDao<CategoryDao>();
                var output = Dao.CreateCategory(inputDto);
                return output;
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
            
        }
    }
}

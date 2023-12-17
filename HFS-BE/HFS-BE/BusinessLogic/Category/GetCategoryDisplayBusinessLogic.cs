using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.Category
{
    public class GetCategoryDisplayBusinessLogic : BaseBusinessLogic
    {
        public GetCategoryDisplayBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListcategoryOutptuDto GetCategoryDisplay()
        {
            try
            {
                var dao = this.CreateDao<CategoryDao>();
                var output = dao.GetCategoryCustomer();
                return output;
            }
            catch (Exception)
            {

                return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CategoryDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Components.Forms;

namespace HFS_BE.BusinessLogic.Category
{
    public class GetAllCategoryBusinessLogic : BaseBusinessLogic
    {
        public GetAllCategoryBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public ListcategoryOutptuDto GetAll()
        {
            try
            {
                var dao = this.CreateDao<CategoryDao>();
                var output = dao.GetAllCategory();
                return output;
            }
            catch (Exception)
            {

                return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.DAO.CategoryDao
{
    public class CategoryDao : BaseDao
    {
        public CategoryDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public CreateCategoryOutputDto CreateCategory (CategoryDaoInputDto inputDto)
        {
            try
            {
                var cate = mapper.Map<CategoryDaoInputDto, Category>(inputDto);
                context.Add(cate);
                context.SaveChanges();
                var output = this.Output<CreateCategoryOutputDto>(Constants.ResultCdSuccess);
                output.CreateCategory = mapper.Map<Category, CategoryDaoOutputDto>(cate);
                return output;
            }
            catch (Exception)
            {

                return this.Output<CreateCategoryOutputDto>(Constants.ResultCdFail);
            }
        }

        public Category? GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(x => x.CategoryId == id); 
        }
    }
}

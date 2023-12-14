using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.CartDao;
using HFS_BE.DAO.VoucherDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.AspNetCore.Components.Forms;

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
                while (true)
                {
                    Category category = new Category()
                    {
                        Name = inputDto.Name,
                        Status = 0
                    };
                    var datacheck = context.Categories.Where(x=>x.CategoryId == category.CategoryId).ToList();
                    if(datacheck.Count == 0)
                    {
                        context.Categories.Add(category);
                        context.SaveChanges();
                        return this.Output<CreateCategoryOutputDto>(Constants.ResultCdSuccess);
                    }
                }
                
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

        public BaseOutputDto UpdateCategory(UpdateCategoryDaoInputDto inputDto)
        {
            try
            {
                var data = context.Categories.FirstOrDefault(x => x.CategoryId == inputDto.CategoryId);
                if(data == null)
                {
                    return this.Output<BaseOutputDto>(Constants.ResultCdFail);
                }
                data.Name= inputDto.Name;
                context.SaveChanges();
                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }

        public BaseOutputDto Enable_Disable_Cate(Enable_Disable_CateInputDto input)
        {
            try
            {
                var data = context.Categories.FirstOrDefault(x => x.CategoryId == input.CategoryId);
                if (data == null)
                {
                    return Output<BaseOutputDto>(Constants.ResultCdFail, $"CategoryId: {input.CategoryId} not exist!");
                }
                if (input.Type)
                {
                    data.Status = 1;
                    context.SaveChanges();
                    return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
                }
                data.Status = 0;
                context.SaveChanges();

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
            

        }

        public ListcategoryOutptuDto GetAllCategory()
        {
            try
            {
                var data = context.Categories.ToList();
                if (data == null)
                {
                    return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
                }
                var output = this.Output<ListcategoryOutptuDto>(Constants.ResultCdSuccess);
                output.ListCategory = mapper.Map<List<Category>, List<GetCategoryOutputDto>>(data);
                return output;
            }
            catch (Exception)
            {

                return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
            }
        }

        public ListcategoryOutptuDto GetCategoryCustomer()
        {
            try
            {
                var data = context.Categories.Where(x => x.Status == 1).ToList();
                if (data == null)
                {
                    return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
                }
                var output = this.Output<ListcategoryOutptuDto>(Constants.ResultCdSuccess);
                output.ListCategory = mapper.Map<List<Category>, List<GetCategoryOutputDto>>(data);
                return output;
            }
            catch (Exception)
            {

                return this.Output<ListcategoryOutptuDto>(Constants.ResultCdFail);
            }
        }
    }
}

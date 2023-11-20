namespace HFS_BE.DAO.CategoryDao
{
    public class CategoryDaoInputDto
    {
        public string Name { get; set; }
        
    }

    public class UpdateCategoryDaoInputDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }

    public class GetAllCategoryDaoInputDto
    {
        public string Admin { get; set; }

    }
    public class Enable_Disable_CateInputDto
    {
        public int CategoryId { get; set; }
        public bool Type { get; set; }
    }
}

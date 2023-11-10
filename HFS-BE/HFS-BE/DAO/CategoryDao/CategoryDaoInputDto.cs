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
        public byte? Status { get; set; }
    }
}

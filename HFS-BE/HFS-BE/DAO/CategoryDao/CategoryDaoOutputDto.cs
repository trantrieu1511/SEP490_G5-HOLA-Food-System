﻿using HFS_BE.Base;

namespace HFS_BE.DAO.CategoryDao
{
    public class CategoryDaoOutputDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public byte? Status { get; set; }
    }
    public class CreateCategoryOutputDto: BaseOutputDto
    {
        public CategoryDaoOutputDto CreateCategory { get; set; }
    }

    public class GetCategoryOutputDto
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
    }

    public class ListcategoryOutptuDto : BaseOutputDto
    {
        public List<GetCategoryOutputDto> ListCategory { get; set; }
    }

}

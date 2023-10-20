using Mailjet.Client.Resources;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HFS_BE.Controllers.ManageFood
{
    public class FoodCreateInputDto
    {
        //[MinLength(1, ErrorMessage = "Name can not empty")]
        public string? Name { get; set; }

        //[MinLength(1, ErrorMessage = "Unit price can not empty")]
        //[Range(0, int.MaxValue, ErrorMessage = "Unit price cannot have a negative value")]
        public decimal UnitPrice { get; set; }

        //[MinLength(1, ErrorMessage = "Description can not empty")]
        public string? Description { get; set; }

        //[MinLength(1, ErrorMessage = "CategoryId can not empty")]
        public int CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Images { get; set; }

    }

    public class FoodUpdateInputDto
    {
        public int FoodId { get; set; }
        //[MinLength(1, ErrorMessage = "Name can not empty")]
        public string? Name { get; set; }

        //[MinLength(1, ErrorMessage = "Unit price can not empty")]
        //[Range(0, int.MaxValue, ErrorMessage = "Unit price cannot have a negative value")]
        public decimal UnitPrice { get; set; }

        //[MinLength(1, ErrorMessage = "Description can not empty")]
        public string? Description { get; set; }

        //[MinLength(1, ErrorMessage = "CategoryId can not empty")]
        public int CategoryId { get; set; }
        public IReadOnlyList<IFormFile>? Images { get; set; }

    }
}

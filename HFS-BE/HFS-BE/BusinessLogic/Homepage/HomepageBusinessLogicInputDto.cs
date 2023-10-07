using HFS_BE.Base;
using HFS_BE.Models;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.BusinessLogic.Homepage
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchShopBusinessLogicInputDto : BaseInputDto
    {
        [Required(ErrorMessage = "Name required")] // back-end validate
        public string Name { get; set; }
    }
}

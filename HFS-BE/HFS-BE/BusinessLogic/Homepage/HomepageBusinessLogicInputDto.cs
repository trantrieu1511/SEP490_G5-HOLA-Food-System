using HFS_BE.Base;
using HFS_BE.Models;
using System.ComponentModel.DataAnnotations;

namespace HFS_BE.Business.ViewShop
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

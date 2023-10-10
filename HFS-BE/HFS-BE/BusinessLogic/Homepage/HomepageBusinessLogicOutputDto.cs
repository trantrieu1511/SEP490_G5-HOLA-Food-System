﻿using HFS_BE.Base;

namespace HFS_BE.BusinessLogic.Homepage
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SearchShopOututDto
    {
        public List<UserDto> ListUser { get; set; } = new List<UserDto>();
    }

    public class ShopDto
    {
        public int UserId { get; set; }
        public string ShopName { get; set; }
        public string ShopAddress { get; set; }
        public string Avatar { get; set; }
    }

    /// <summary>
    /// Output of DisplayShop.
    /// </summary>
    public class DisplayShopOutputDto : BaseOutputDto
    {
        public List<ShopDto> ListShop { get; set; }
        public int total { get; set; }
    }
}

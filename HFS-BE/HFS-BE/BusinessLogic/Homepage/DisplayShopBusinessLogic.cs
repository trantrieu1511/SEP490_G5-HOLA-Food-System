﻿using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.ShopDao;
using HFS_BE.Models;

namespace HFS_BE.BusinessLogic.Homepage
{
    public class DisplayShopBusinessLogic : BaseBusinessLogic
    {
        public DisplayShopBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public DisplayShopOutputDto DisplayShop()
        {
            try
            {
                var Dao = this.CreateDao<ShopDao>();
                Dao.ShopDao.DisplayShopDaoOutputDto daooutput = Dao.DisplayShop();
                var output = mapper.Map<Dao.ShopDao.DisplayShopDaoOutputDto, DisplayShopOutputDto>(daooutput);
                return output;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

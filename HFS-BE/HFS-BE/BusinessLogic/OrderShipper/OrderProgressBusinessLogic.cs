using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Controllers.OrderShipper;
using HFS_BE.Dao.OrderDao;
using HFS_BE.DAO.OrderProgressDao;
using HFS_BE.Models;
using HFS_BE.Utils;
using HFS_BE.Utils.IOFile;

namespace HFS_BE.BusinessLogic.OrderShipper
{
    public class OrderProgressBusinessLogic : BaseBusinessLogic
    {
        public OrderProgressBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }
        public BaseOutputDto CreateOrderProgress(OrderProgressBusinessLogicInputDto inputDto)
        {
            try
            {
                string fileNames = null;
                if (inputDto.Image != null)
                {
                    fileNames = ReadSaveImage.SaveImagesOrderProgress(inputDto.Image, inputDto.UserDto, 2);
                   
                }
                var dao = this.CreateDao<OrderProgressDao>();
                var inputMapper = mapper.Map<OrderProgressBusinessLogicInputDto, DAO.OrderProgressDao.OrderProgressDaoInputDto>(inputDto);
                inputMapper.Image = fileNames;
                BaseOutputDto baseOutputDto = dao.CreateOrderProgress(inputMapper);
                return baseOutputDto;
                
               
            }
            catch (Exception)
            {

                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

using HFS_BE.Models;

namespace HFS_BE.Base
{
    public class BaseBusinessLogic
    {
        private readonly SEP490_HFSContext context;
        public BaseBusinessLogic(SEP490_HFSContext context) {
            this.context = context;
        }
        public T CreateDao<T>() where T : BaseDao
        {
            return (T)Activator.CreateInstance(typeof(T), context);
        }

        //public BaseOutputDto Output(bool success)
        //{
        //    BaseOutputDto outputDto = new BaseOutputDto();
        //    outputDto.Success = success;
        //    return outputDto;
        //}
    }
}

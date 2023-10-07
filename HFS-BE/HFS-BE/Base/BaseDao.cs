using HFS_BE.Models;

namespace HFS_BE.Base
{
    public class BaseDao
    {
        public readonly SEP490_HFSContext context;
        public BaseDao(SEP490_HFSContext context)
        {
            this.context = context;
        }
    }
}

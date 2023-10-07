using HFS_BE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.Base
{
    public class BaseController : ControllerBase
    {
        private readonly SEP490_HFSContext context;

        public BaseController(SEP490_HFSContext context)
        {
            this.context = context;
        }

        public T GetBusiness<T>() where T : BaseBusinessLogic
        {
            return (T)Activator.CreateInstance(typeof(T), context);
        }
    }
}

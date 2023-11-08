using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Models;
using HFS_BE.Utils;
using Microsoft.EntityFrameworkCore;

namespace HFS_BE.DAO.NewsfeedDao
{
    public class NewsfeedDao : BaseDao
    {
        public NewsfeedDao(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public ListNewsfeedOutputDto GetAllNews()
        {
            try
            {
                var news = context.Posts
                    .Include(p => p.Seller)
                    .Include(p => p.PostImages)
                    .Select(p => new NewsfeedOutputDto
                    {
                        PostId = p.PostId,
                        SellerId = p.SellerId,
                        SellerFullName = p.Seller.LastName + " " + p.Seller.FirstName,
                        PostContent= p.PostContent,
                        CreatedDate = p.CreatedDate,
                        Images = p.PostImages.ToList(),
                    })
                    .OrderByDescending(p => p.CreatedDate)
                    .ToList();
                var output = Output<ListNewsfeedOutputDto>(Constants.ResultCdSuccess);
                output.News = news;
                return output;
            }
            catch (Exception e)
            {
                return Output<ListNewsfeedOutputDto>(Constants.ResultCdFail, e.Message + e.Source + e.StackTrace + e.InnerException);
            }
        }
    }
}

using AutoMapper;
using HFS_BE.Base;
using HFS_BE.DAO.FeedBackVoteDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.FoodDetail
{
    public class VoteFeedBackBusinessLogic : BaseBusinessLogic
    {
        public VoteFeedBackBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto VoteFeedBack(VoteDaoInputDto inputDto)
        {
            try
            {
                var voteDao = this.CreateDao<FeedBackVoteDao>();
                var getvote = voteDao.GetVote(inputDto);

                if (getvote.FeedBackId == null)
                {
                    voteDao.CreateVote(inputDto);
                }
                else
                {
                    if (inputDto.IsLike == null)
                    {
                        voteDao.DeleteVote(inputDto);
                    }
                    else
                    {
                        voteDao.UpdadteVote(inputDto);
                    }
                }

                return this.Output<BaseOutputDto>(Constants.ResultCdSuccess);
            }
            catch (Exception)
            {
                return this.Output<BaseOutputDto>(Constants.ResultCdFail);
            }
        }
    }
}

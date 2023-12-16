using AutoMapper;
using HFS_BE.Base;
using HFS_BE.Dao.PostDao;
using HFS_BE.DAO.PostVoteDao;
using HFS_BE.Models;
using HFS_BE.Utils;

namespace HFS_BE.BusinessLogic.ManagePost
{
    public class VotePostBusinessLogic : BaseBusinessLogic
    {
        public VotePostBusinessLogic(SEP490_HFS_2Context context, IMapper mapper) : base(context, mapper)
        {
        }

        public BaseOutputDto Vote(PostVoteDaoInputDto inputDto)
        {
            try
            {
                var voteDao = this.CreateDao<PostVoteDao>();
                var getvote = voteDao.GetVote(inputDto);

                if (getvote.PostId == null)
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
